using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        // gets a list of partial reviews
        [HttpGet]
        public ActionResult Get([FromQuery(Name = "sortDirection")] String sortDirection = "asc", [FromQuery(Name = "sortField")] String sortField = "Time", [FromQuery(Name = "filterFields")] string filterFields = null, [FromQuery(Name = "filterValues")] string filterValues = null, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = 10, [FromBody] NullableGuidDeserializer nullableUserId = null)
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
                return BadRequest(failure);
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
                return StatusCode(500, failure);
            }

            if (records.Count == 0)
            {
                Failure failure = FailureFact.NoRecordsFound(null, userId);
                db.InsertRecord("Failures", failure);
                return NotFound(failure);
            }

            List<PartialReview> partialRecords = new List<PartialReview>();
            foreach (Review record in records)
            {
                partialRecords.Add(record.ToPartialReview());
            }

            Success success = SuccessFact.AllReviewsRetrieved(userId);
            db.InsertRecord("Successes", success);

            return new OkObjectResult(partialRecords);
        }

        // get the one full review
        [HttpGet("{reviewId}", Name = "Get")]
        public ActionResult Get(Guid reviewId, [FromBody] NullableGuidDeserializer nullableUserId = null)
        {

            Guid? userId = nullableUserId.Property;

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return NotFound(failure);
            }

            Success success = SuccessFact.ReviewRetrieved(reviewId, userId);
            db.InsertRecord("Successes", success);

            return new OkObjectResult(record);
        }

        // creates a review
        [HttpPost]
        public ActionResult Post([FromBody] Review value)
        {
            try
            {
                db.InsertRecord("Reviews", value);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, value.UserId);
                db.InsertRecord("Failures", failure);
                return StatusCode(500, failure);
            }

            Success success = SuccessFact.ReviewCreated(value.ReviewId, value.UserId);
            db.InsertRecord("Successes", success);
            return new OkObjectResult(success);
        }

        // updates sections in a review
        [HttpPut("{reviewId}")]
        public ActionResult Put(Guid reviewId, [FromBody] JObject value)
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
                return BadRequest(failure);
            }

            List<Section> sections = null;
            List<Guid> comments = null;
            try
            {
                if(value["sections"] != null)
                {
                    sections = value["sections"].ToObject<List<Section>>();
                }
                else
                {
                    comments = value["comments"].ToObject<List<Guid>>();
                }
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadRequestBody(e, userId);
                db.InsertRecord("Failures", failure);
                return BadRequest(failure);
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
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return NotFound(failure);
            }

            if (sections != null)
            {
                record.SetSection(sections, userId);
            }
            else
            {
                record.SetComments(comments, userId);
            }

            db.PutRecord("Reviews", value, reviewId);

            Success success = SuccessFact.ReviewSectionsModified(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new OkObjectResult(success);
        }

        // Soft deletes a review
        [HttpDelete("{reviewId}")]
        public ActionResult Delete(Guid reviewId, [FromBody] JObject value)
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
                return BadRequest(failure);
            }

            Review record;
            try
            {
                record = db.FindRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadUserId(e, null);
                db.InsertRecord("Failures", failure);
                return BadRequest(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return NotFound(failure);
            }
            else
            {
                record.Delete(Guid.Empty);
                db.PutRecord("Reviews", record, reviewId);
            }

            Success success = SuccessFact.ReviewSoftDelete(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new OkObjectResult(success);
        }

        //Un-deletes a soft deleted review
        [HttpPatch("{reviewId}")]
        public ActionResult Patch(Guid reviewId, [FromBody] JObject value)
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
                return BadRequest(failure);
            }

            Review record;
            try
            {
                record = db.FindDeletedRecordById<Review>("Reviews", reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord("Failures", failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId);
                db.InsertRecord("Failures", failure);
                return NotFound(failure);
            }
            else
            {
                record.ReInstate(Guid.Empty);
                db.PutRecord("Reviews", record, reviewId);
            }

            Success success = SuccessFact.ReviewReinstated(reviewId, userId);
            db.InsertRecord("Successes", success);
            return new OkObjectResult(success);
        }
    }
}