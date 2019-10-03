﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Review_Api.Database
{
    public class ReviewDB
    {
        private readonly IMongoDatabase db;

        public ReviewDB(string database)
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

        public T FindRecordById<T>(string table, Guid Id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", Id);
            return collection.Find(filter).FirstOrDefault();
        }

    }
}
