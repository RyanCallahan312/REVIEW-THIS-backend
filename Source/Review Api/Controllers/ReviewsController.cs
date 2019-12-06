using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using Review_Api.ModelFactory;
using Review_Api.Database;
using Review_Api.Models.Query;
using Review_Api.Util;
using Review_Api.Models;
using Newtonsoft.Json.Linq;
using Review_Api.Models.Response;

namespace Review_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewDB db = new ReviewDB("movie-review");
        // GET: api/Reviews
        [HttpGet]
        public JsonResult Get([FromQuery(Name = "sortDirection")] String sortDirection = "asc", [FromQuery(Name = "sortField")] String sortField = "Time", [FromQuery(Name = "filterFields")] string filterFields = null, [FromQuery(Name = "filterValues")] string filterValues = null, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = 10)
        {
            Sort sort = ParseQuery.ParseSort(sortDirection, sortField);
            Page page = ParseQuery.ParsePage(pageNumber, pageItems);

            List<Filter> filters;
            try
            {
                filters = ParseQuery.ParseFilters(filterFields, filterValues);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.UnevenFilters(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            List<Review> records;
            try
            {
                records = db.LoadRecords<Review>("Reviews", filters, sort, page);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            if (records.Count == 0)
            {
                Failure failure = FailureFact.NoRecordsFound(null);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            List<PartialReview> partialRecords = new List<PartialReview>();
            foreach (Review record in records)
            {
                partialRecords.Add(record.ToPartialReview());
            }

            return new JsonResult(partialRecords);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(Guid id)
        {

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", id);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            return new JsonResult(record);
        }

        // POST: api/Reviews
        [HttpPost]
        public JsonResult Post([FromBody] Review value)
        {
            db.InsertRecord("Reviews", value);
            return new JsonResult(SuccessFact.Default(value.ReviewId));
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public JsonResult Put(Guid id, [FromBody] JObject value)
        {

            List<Section> sections;
            try
            {
                sections = value["sections"].ToObject<List<Section>>();
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadRequestBody(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            Guid userId;
            try
            {
                userId = value["userId"].ToObject<Guid>();
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadUserId(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", id);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            record.SetSection(sections, userId);

            db.PutRecord("TestTable", value, id);

            return new JsonResult(SuccessFact.Default(id));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", id);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }
            else
            {
                record.Delete(Guid.Empty);
                db.PutRecord("Reviews", record, id);
            }
            return new JsonResult(SuccessFact.Default(id));
        }
        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public JsonResult Patch(Guid id)
        {
            Review record;
            try
            {
                record = db.FindDeletedRecordById<Review>("Reviews", id);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null);
                db.InsertRecord("failures", failure);
                return new JsonResult(failure);
            }
            else
            {
                record.ReInstate(Guid.Empty);
                db.PutRecord("Reviews", record, id);
            }
            return new JsonResult(SuccessFact.Default(id));
        }
    }

    public class Dead
    {
        [BsonId]
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public string TestString { get; set; }

        public Dead(string testString, bool deleted)
        {
            TestString = testString;
            Deleted = deleted;
        }
    }
}