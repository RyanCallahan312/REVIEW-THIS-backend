﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using review_api.ModelFactory;
using review_api.Database;
using review_api.Models.Query;
using review_api.Util;
using review_api.Models;
using Newtonsoft.Json.Linq;
using review_api.Models.Response;

namespace review_api.Controllers
{

    [Route("api-v1/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewDB db = new ReviewDB("movie-review-V1");
        private readonly string REVIEW_TABLE = "Reviews";
        private readonly string FAILURE_TABLE = "Failures";
        private readonly string SUCCESS_TABLE = "Successes";
        private readonly string UNAUTHORIZED_TABLE = "Unauorized";

        // gets a list of partial reviews
        [HttpGet]
        public IActionResult Get([FromQuery(Name = "sortDirection")] string sortDirection = "asc", [FromQuery(Name = "sortField")] string sortField = "Time", [FromQuery(Name = "filterFields")] string filterFields = null, [FromQuery(Name = "filterValues")] string filterValues = null, [FromQuery(Name = "pageNumber")] int pageNumber = 0, [FromQuery(Name = "pageItems")] int pageItems = 10, [FromBody] NullableGuidDeserializer nullableUserId = null)
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
                Failure failure = FailureFact.UnevenFilters(e, userId, filterValues, filterFields);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return BadRequest(failure);
            }

            List<Review> records;
            try
            {
                records = db.LoadRecords<Review>(REVIEW_TABLE, filters, sort, page);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (records.Count == 0)
            {
                Failure failure = FailureFact.NoRecordsFound(null, userId, sort, filters, page);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return NotFound(failure);
            }

            List<PartialReview> partialRecords = new List<PartialReview>();
            foreach (Review record in records)
            {
                partialRecords.Add(record.ToPartialReview());
            }

            Success success = SuccessFact.ReviewsRetrieved(userId, sort, filters, page);
            db.InsertRecordAsync(SUCCESS_TABLE, success);

            return new OkObjectResult(partialRecords);
        }

        // get the one full review
        [HttpGet("{reviewId}")]
        public IActionResult Get(Guid reviewId, [FromBody] NullableGuidDeserializer nullableUserId = null)
        {

            Guid? userId = nullableUserId.Property;

            Review record;
            try
            {
                record = db.FindRecordById<Review>(REVIEW_TABLE, reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId, reviewId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return NotFound(failure);
            }

            Success success = SuccessFact.ReviewRetrieved(reviewId, userId);
            db.InsertRecordAsync(SUCCESS_TABLE, success);

            return new OkObjectResult(record);
        }

        // creates a review
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            try
            {
                db.InsertRecord(REVIEW_TABLE, review);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, review.UserId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            Success success = SuccessFact.ReviewCreated(review.ReviewId, review.UserId);
            db.InsertRecordAsync(SUCCESS_TABLE, success);
            return Created($"https://www.ReviewThis.dev/{GuidToBase64.EncodeBase64String(review.ReviewId)}",success);
        }

        // updates sections in a review
        [HttpPut("{reviewId}")]
        public IActionResult Put(Guid reviewId, [FromBody] JObject value)
        {

            Guid userId;
            try
            {
                userId = value["userId"].ToObject<Guid>();
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadUserId(e, value["userId"]);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
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
                Failure failure = FailureFact.BadRequestBody(e, userId, value["sections"]);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return BadRequest(failure);
            }

            Review record;
            try
            {
                record = db.FindRecordById<Review>(REVIEW_TABLE, reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId, reviewId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
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

            db.PutRecord(REVIEW_TABLE, value, reviewId);

            Success success = SuccessFact.ReviewSectionsModified(reviewId, userId, sections);
            db.InsertRecordAsync(SUCCESS_TABLE, success);

            return NoContent();
        }

        // Soft deletes a review
        [HttpDelete("{reviewId}")]
        public IActionResult Delete(Guid reviewId, [FromQuery] Guid userId)
        {

            Review record;
            try
            {
                record = db.FindRecordById<Review>(REVIEW_TABLE, reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.BadUserId(e, null);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return BadRequest(failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId, reviewId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return NotFound(failure);
            }
            else
            {
                record.Delete(Guid.Empty);
                db.PutRecord(REVIEW_TABLE, record, reviewId);
            }

            Success success = SuccessFact.ReviewSoftDelete(reviewId, userId);
            db.InsertRecordAsync(SUCCESS_TABLE, success);

            return NoContent();
        }

        //Un-deletes a soft deleted review
        [HttpPatch("{reviewId}")]
        public IActionResult Patch(Guid reviewId, [FromQuery] Guid userId)
        {

            Review record;
            try
            {
                record = db.FindDeletedRecordById<Review>(REVIEW_TABLE, reviewId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId, reviewId);
                db.InsertRecordAsync(FAILURE_TABLE, failure);
                return NotFound(failure);
            }
            else
            {
                record.ReInstate(Guid.Empty);
                db.PutRecord(REVIEW_TABLE, record, reviewId);
            }

            Success success = SuccessFact.ReviewReinstated(reviewId, userId);
            db.InsertRecordAsync(SUCCESS_TABLE, success);

            return NoContent();
        }
    }
}