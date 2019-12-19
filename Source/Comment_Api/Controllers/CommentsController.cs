using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comment_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        // GET: api/Comments
        [HttpGet("{reviewId}")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comments/5
        [HttpGet("{reivewId}/{commentId}")]
        public string Get(Guid reviewId, Guid commentId)
        {
            return "value";
        }

        // POST: api/Comments
        [HttpPost("{reviewId}")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Comments/5
        [HttpPut("{commentId}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{reviewId}/{commentId}")]
        public void Delete(int id)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpPatch("{reviewId}/{commentId}")]
        public void Patch(int id)
        {
        }
    }
}
