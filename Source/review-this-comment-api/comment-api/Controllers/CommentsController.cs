﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using comment_api.Database;
using comment_api.Models;
using comment_api.Models.Response;
using comment_api.ModelFactory;
using review_api.Models.Query;
using review_api.Util;

namespace comment_api.Controllers
{
    [Route("api-v1/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentDb db = new CommentDb("movie-comment-V1");
        private readonly string COMMENT_TABLE = "Comments";
        private readonly string FAILURE_TABLE = "Failures";
        private readonly string SUCCESS_TABLE = "Successes";
        private readonly string UNAUTHORIZED_TABLE = "Unauorized";

        // GET: api-v1/comments/by-review/{reviewId}
        // gets all comments for a review
        [HttpGet("by-review/{reviewId}")]
        public ActionResult GetByReview(Guid reviewId, [FromBody] NullableGuidDeserializer nullableUserId = null)
        {

            Guid? userId = nullableUserId.Property;

            List<Comment> records;
            List<Filter> filters = ParseQuery.ParseFilters("ReivewId",reviewId.ToString());

            try
            {
                records = db.LoadRecords<Comment>(COMMENT_TABLE, filters, null, null) ;
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (records == null)
            {
                Failure failure = FailureFact.NoRecordsFound(null, userId, null, filters, null);
                db.InsertRecord(FAILURE_TABLE, failure);
                return NotFound(failure);
            }

            Success success = SuccessFact.CommentsByReviewRetrieved(reviewId, userId);
            db.InsertRecord(SUCCESS_TABLE, success);
            return new OkObjectResult(records);
        }

        // POST: api-v1/comments/by-review/{reviewId}
        // create a new comment in on a review
        [HttpPost("by-review/{reviewId}")]
        public ActionResult PostOnReview(Guid reviewId, [FromBody] Comment comment)
        {
            return Ok();
        }

        // GET: api-v1/comments/{commentId}
        // get one specific comment on a review
        [HttpGet("{commentId}")]
        public ActionResult GetByComment(Guid commentId, [FromBody] NullableGuidDeserializer nullableUserId = null)
        {

            Guid? userId = nullableUserId.Property;

            Comment record;

            try
            {
                record = db.FindRecordById<Comment>(COMMENT_TABLE, commentId);
            }
            catch (Exception e)
            {
                Failure failure = FailureFact.Default(e, userId);
                db.InsertRecord(FAILURE_TABLE, failure);
                return StatusCode(500, failure);
            }

            if (record == null)
            {
                Failure failure = FailureFact.IdNotFound(null, userId, commentId);
                db.InsertRecord(FAILURE_TABLE, failure);
                return NotFound(failure);
            }

            Success success = SuccessFact.CommentRetrieved(commentId, userId);
            db.InsertRecord(SUCCESS_TABLE, success);
            return new OkObjectResult(record);
        }

        // POST: api-v1/comments/{parentId}
        // create a new comment in on a review
        [HttpPost("{parentId}")]
        public ActionResult PostOnComment(Guid parentId, [FromBody] Comment comment)
        {
            return Ok();
        }

        // PUT: api-v1/comments/{commentId}
        // edit a comment
        [HttpPut("{commentId}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE: api-v1/comments/{commentId}
        // delete a comment off of a review
        [HttpDelete("{commentId}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // PATCH: api-v1/comments/{commentId}
        // un-delete a single comment
        [HttpPatch("{commentId}")]
        public ActionResult Patch(int id)
        {
            return Ok();
        }
    }
}
