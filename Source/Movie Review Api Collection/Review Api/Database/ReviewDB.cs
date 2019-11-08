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
            FilterDefinition<T> mongoFilters;
            SortDefinition<T> mongoSort;
            if (filters.Count > 0)
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
            List<T> data = collection.Find(mongoFilters).Sort(mongoSort).Skip(page.PageNumber * page.ItemsPerPage).Limit(page.ItemsPerPage).ToList();
            data.AsQueryable().OrderBy(e => GetReflectedPropertyValue(e,sort.Field).ToUpper());
            return data;
        }

        public T FindRecordById<T>(string table, Guid Id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", Id);
            return collection.Find(filter).FirstOrDefault();
        }

        public static string GetReflectedPropertyValue(object subject, string field)
        {
            object reflectedValue = subject.GetType().GetProperty(field).GetValue(subject, null);
            return reflectedValue != null ? reflectedValue.ToString() : "";
        }

    }
}
