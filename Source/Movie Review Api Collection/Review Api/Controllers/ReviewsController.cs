using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Review_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        // GET: api/Reviews
        [HttpGet]
        public string Get()
        {
            return "get";
        }

        // GET: api/Reviews/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST: api/Reviews
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return $"value {value}";
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return $"value {value} + {id}";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"value {id}";
        }
        // DELETE: api/ApiWithActions/5
        [HttpPatch("{id}")]
        public string Patch(int id)
        {
            return $"value {id}";
        }
    }
}
