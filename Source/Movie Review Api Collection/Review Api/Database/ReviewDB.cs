using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Review_Api.Models.Query;

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

        public List<T> LoadRecords<T>(string table, List<Filter> filters, Sort sort, Page page)
        {
            var collection = db.GetCollection<T>(table);
            FilterDefinition<T> mongoFilters = null;
            SortDefinition<T> mongoSort = null;
            if (filters != null)
            {
                var queryBuilder = Builders<T>.Filter;
                mongoFilters = queryBuilder.Eq(filters[0].field, filters[0].value);
                foreach (Filter filter in filters)
                {
                    mongoFilters = mongoFilters | queryBuilder.Eq(filter.field, filter.value);
                }
            }
            else
            {
                mongoFilters = new BsonDocument();
            }

            if (sort.Direction == "asc")
            {
                mongoSort = Builders<T>.Sort.Ascending(sort.Field);
            }
            else
            {
                mongoSort = Builders<T>.Sort.Descending(sort.Field);

            }
            return collection.Find(mongoFilters).Sort(mongoSort).Skip(page.PageNumber * page.ItemsPerPage).Limit(page.ItemsPerPage).ToList();
        }

        public T FindRecordById<T>(string table, Guid Id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", Id);
            return collection.Find(filter).FirstOrDefault();
        }

    }
}
