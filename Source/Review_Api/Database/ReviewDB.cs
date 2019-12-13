using System;
using System.Collections.Generic;
using System.Linq;
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

        public void PutRecord<T>(string table, T record, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var queryBuilder = Builders<T>.Filter;
            FilterDefinition<T> mongoFilters = queryBuilder.Eq("_id", id);
            collection.ReplaceOne(mongoFilters, record);
        }

        public List<T> LoadRecords<T>(string table, List<Filter> filters, Sort sort, Page page)
        {
            var collection = db.GetCollection<T>(table);
            FilterDefinition<T> mongoFilters;

            if (filters.Count > 0)
            {
                var queryBuilder = Builders<T>.Filter;
                mongoFilters = queryBuilder.Eq(filters[0].Field, filters[0].Value);

                foreach (Filter filter in filters)
                {
                    mongoFilters |= queryBuilder.Eq(filter.Field, filter.Value);
                }

                mongoFilters |= queryBuilder.Eq("Deleted", false);
            }
            else
            {
                mongoFilters = Builders<T>.Filter.Eq("Deleted", false);
            }

            List<T> data = collection.Find(mongoFilters).ToList();

            if (sort.Direction == "asc")
            {
                data = data.AsQueryable().OrderBy(e => GetReflectedPropertyValue(e, sort.Field).ToUpper()).Skip(page.PageNumber * page.ItemsPerPage).Take(page.ItemsPerPage).ToList();
            }
            else
            {
                data = data.AsQueryable().OrderByDescending(e => GetReflectedPropertyValue(e, sort.Field).ToUpper()).Skip(page.PageNumber * page.ItemsPerPage).Take(page.ItemsPerPage).ToList();
            }

            return data;
        }

        public T FindRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var queryBuilder = Builders<T>.Filter;
            var mongoFilters = queryBuilder.Eq("_id", id);
            mongoFilters &= queryBuilder.Eq("Deleted", false);
            return collection.Find(mongoFilters).FirstOrDefault();
        }

        public T FindDeletedRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        public string RemoveRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return collection.DeleteOne(filter).ToString();
        }

        public static string GetReflectedPropertyValue(object subject, string field)
        {
            object reflectedValue = subject.GetType().GetProperty(field).GetValue(subject, null);
            return reflectedValue != null ? reflectedValue.ToString() : "";
        }

    }
}
