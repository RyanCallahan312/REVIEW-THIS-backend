using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Review_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private MongoCRUD db = new MongoCRUD("MovieTest");
        // GET: api/Reviews
        [HttpGet]
        public string Get()
        {
            var records = db.LoadRecords<Tester>("TestTable");
            string str = "";
            foreach(Tester record in records){
                str += record.TestString + "\n";
            }
            return str;
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST: api/Reviews
        [HttpPost]
        public string Post([FromBody] Tester value)
        {
            this.db.InsertRecord("TestTable", value);
            return $"posted {value}";
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return $"value {value} + {id}";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"value {id}";
        }
        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public string Patch(int id)
        {
            return $"value {id}";
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
        public ObjectId _id { get; set; }
        public string TestString { get; set; }

        public Tester(string testString)
        {

            TestString = testString;
        }
    }
}
