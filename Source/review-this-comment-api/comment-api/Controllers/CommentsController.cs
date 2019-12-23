using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comment_api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        // GET: api/Comments/review
        // gets all comments for a review
        [HttpGet("review/{reviewId}")]
        public ActionResult GetByReview(Guid reviewId)
        {
            return Ok();
        }

        // GET: api/Comments/5
        // get one specific comment on a review
        [HttpGet("{commentId}")]
        public ActionResult Get(Guid commentId)
        {
            return Ok();
        }

        // POST: api/Comments/review
        // create a new comment in on a review
        [HttpPost("review")]
        public ActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT: api/Comments/5
        // edit a comment
        [HttpPut("{commentId}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        // delete a comment off of a review
        [HttpDelete("{commentId}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // PATCH: api/ApiWithActions/5
        // un-delete a single comment
        [HttpPatch("{commentId}")]
        public ActionResult Patch(int id)
        {
            return Ok();
        }
    }
}
