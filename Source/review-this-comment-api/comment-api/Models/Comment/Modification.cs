using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comment_api.Models
{
    public class Modification
    {
        [BsonRequired]
        public DateTime Time { get; set; }
        [BsonRequired]
        public string Body { get; set; }
        [BsonRequired]
        public Guid UserId { get; set;  }

        public Modification(DateTime time, string body, Guid userId)
        {
            Time = time;
            Body = body;
            UserId = userId;
        }
    }
}
