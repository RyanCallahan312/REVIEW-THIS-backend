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
        public JsonResult Get([FromQuery(Name = "sortDirection")] String sortDirection = "asc", [FromQuery(Name = "sortField")] String sortField = "Time", [FromQuery(Name = "filterFields")] string filterFields = null, [FromQuery(Name = "filterValues")] string filterValues = null, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = 10, [FromBody] NullableGuidDeserializer nullableUserId = null)
        {
            Guid? userId = nullableUserId.Property;

            Sort sort = ParseQuery.ParseSort(sortDirection, sortField);
            Page page = ParseQuery.ParsePage(pageNumber, pageItems);

            List<Filter> filters;
            try
            {
                filters = ParseQuery.ParseFilters(filterFields, filterValues);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.UnevenFilters(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            List<Review> records;
            try
            {
                records = db.LoadRecords<Review>("Reviews", filters, sort, page);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            if (records.Count == 0)
            {
                Failure failure = FailureFact.NoRecordsFound(null, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            List<PartialReview> partialRecords = new List<PartialReview>();
            foreach (Review record in records)
            {
                partialRecords.Add(record.ToPartialReview());
            }

            Success success = SuccessFact.AllReviewsRetrieved(userId);
            db.InsertRecord("Successes", success);

            return new JsonResult(partialRecords);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(Guid reviewId, [FromBody] Guid? userId = null)
        {

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            Success success = SuccessFact.ReviewRetrieved(reviewId, userId);
            db.InsertRecord("Successes", success);

            return new JsonResult(record);
        }

        // POST: api/Reviews
        [HttpPost]
        public JsonResult Post([FromBody] Review value)
        {
            db.InsertRecord("Reviews", value);
            Success success = SuccessFact.ReviewCreated(value.ReviewId, value.UserId);
            db.InsertRecord("Successes", success);
            return new JsonResult(success);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public JsonResult Put(Guid reviewId, [FromBody] JObject value)
        {

            Guid userId;
            try
            {
                userId = value["userId"].ToObject<Guid>();
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadUserId(e, null);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            List<Section> sections;
            try
            {
                sections = value["sections"].ToObject<List<Section>>();
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadRequestBody(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            record.SetSection(sections, userId);

            db.PutRecord("Reviews", value, reviewId);

            Success success = SuccessFact.ReviewSectionsModified(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new JsonResult(success);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public JsonResult Delete(Guid reviewId, [FromBody] Guid? userId = null)
        {
            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }
            else
            {
                record.Delete(Guid.Empty);
                db.PutRecord("Reviews", record, reviewId);
            }

            Success success = SuccessFact.ReviewSoftDelete(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new JsonResult(success);
        }
        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public JsonResult Patch(Guid reviewId, [FromBody] Guid? userId = null)
        {
            Review record;
            try
            {
                record = db.FindDeletedRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return new JsonResult(failure);
            }
            else
            {
                record.ReInstate(Guid.Empty);
                db.PutRecord("Reviews", record, reviewId);
            }

            Success success = SuccessFact.ReviewReinstated(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new JsonResult(success);
        }
    }
}