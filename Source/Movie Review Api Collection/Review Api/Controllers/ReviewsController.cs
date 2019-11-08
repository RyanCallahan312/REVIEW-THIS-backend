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
        public JsonResult Search([FromQuery(Name = "sortDirection")] String sortDirection = "asc", [FromQuery(Name = "sortField")] String sortField = "TimeCreated", [FromQuery(Name = "filterFields")] string filterFields = null, [FromQuery(Name = "filterValues")] string filterValues = null, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = 0)
        {

            Sort sort = ParseQuery.ParseSort(sortDirection, sortField);
            List<Filter> filters;
            try
            {
                filters = ParseQuery.ParseFilters(filterFields, filterValues);
            }
            catch
            {
                return new JsonResult(FailureFact.UnevenFilters());
            }
            Page page = ParseQuery.ParsePage(pageNumber, pageItems);
            Console.WriteLine(sort.ToString(), filters.ToString(), page.ToString());
            List<Tester> records;

            try
            {
                records = db.LoadRecords<Tester>("TestTable", filters, sort, page);
            }
            catch
            {
                return new JsonResult(FailureFact.Default());
            }

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