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
using Review_Api.Models.Response;
using Review_Api.ModelFactory;
using Review_Api.Database;
using Review_Api.Models.Query;

namespace Review_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewDB db = new ReviewDB("MovieTest");
        // GET: api/Reviews
        [HttpGet]
        public JsonResult Get([FromRoute] Sort sort,[FromRoute] Filter filters,[FromRoute] Page page)
        {
            var print = sort.ToString() + filters.ToString() + page.ToString();
            Console.WriteLine(print);
            List<Tester> records;

            try
            {
                records = db.LoadRecords<Tester>("TestTable");
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