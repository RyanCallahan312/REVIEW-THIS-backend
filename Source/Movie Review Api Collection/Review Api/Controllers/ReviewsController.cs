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

namespace Review_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private MongoCRUD db = new MongoCRUD("MovieTest");
        // GET: api/Reviews
        [HttpGet]
        public JsonResult Get()
        {
            List<Tester> records = db.LoadRecords<Tester>("TestTable");
            return new JsonResult(records);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return new JsonResult($"value {id}");
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
        public JsonResult Put(int id, [FromBody] string value)
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

    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
    public class Tester
    {
        [BsonId]
        public Guid _id { get; set; }

        public string TestString { get; set; }

        public Tester(string testString)
        {

            TestString = testString;
        }
    }
}