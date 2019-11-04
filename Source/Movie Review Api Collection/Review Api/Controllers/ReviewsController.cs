using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Review_Api.ModelFactory;
using Review_Api.Database;
using Review_Api.Models.Query;
using Review_Api.Util;

namespace Review_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewDB db = new ReviewDB("MovieTest");
        // GET: api/Reviews
        [HttpGet]
        public JsonResult Search([FromQuery(Name = "sortDirection")] String sortDirection, [FromQuery(Name = "sortField")] String sortField, [FromQuery(Name = "filterFields")] string filterFields, [FromQuery(Name = "filterValues")] string filterValues, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = -1)
        {
            Sort sort = ParseQuery.ParseSort(sortDirection, sortField);
            List<Filter> filters = ParseQuery.ParseFilters(filterFields, filterValues);
            Page page = ParseQuery.ParsePage(pageNumber, pageItems);
            Console.WriteLine(sort.ToString(), filters.ToString(), page.ToString());
            List<Tester> records;

            try
            {
                records = db.LoadRecords<Tester>("TestTable", filters);
            }
            catch
            {
                return new JsonResult(FailureFact.Default());
            }

            //records = Query.Filter(records, filters);

            records = Query.Paginate(records, page);

            return new JsonResult(records);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(string id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
            {
                return new JsonResult(FailureFact.BadId());
            }
            Tester record = db.FindRecordById<Tester>("TestTable", parsedId);
            if (record == null)
            {
                return new JsonResult(FailureFact.IdNotFound());
            }
            return new JsonResult(record);
        }

        // POST: api/Reviews
        [HttpPost]
        public JsonResult Post([FromBody] Tester value)
        {
            this.db.InsertRecord("TestTable", value);
            return new JsonResult($"posted {value.TestString}");
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public JsonResult Put(Guid id, [FromBody] string value)
        {
            return new JsonResult($"value {value} + {id}");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            return new JsonResult($"value {id}");
        }
        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public JsonResult Patch(int id)
        {
            return new JsonResult($"value {id}");
        }
    }

    public class Query
    {
        public static List<Tester> Paginate(List<Tester> content, Page page)
        {
            if (page.ItemsPerPage == -1 || page.PageNumber == -1)
            {
                return content;
            }
            if (content.Count < page.PageNumber * page.ItemsPerPage)
            {
                return new List<Tester>();
            }
            return content.Skip(page.PageNumber * page.ItemsPerPage).Take(page.ItemsPerPage).ToList();
        }
        public static List<Tester> Filter(List<Tester> content, List<Filter> filters)
        {
            foreach (Filter filter in filters)
            {
                content = content.Where(item => item.TestString != filter.value).ToList();
            }
            return content;
        }
    }
    public class Tester
    {
        [BsonId]
        public Guid Id { get; set; }

        public string TestString { get; set; }

        public Tester(string testString)
        {

            TestString = testString;
        }
    }
}