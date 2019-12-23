using System;
using MongoDB.Bson.Serialization.Attributes;

namespace comment_api.Models
{
    public class Deletion
    {
        [BsonRequired]
        public Guid UserId { get; set; }
        [BsonRequired]
        public bool Deleted { get; set; }
        [BsonRequired]
        public DateTime Time { get; set;  }

        public Deletion(Guid userId, bool deleted, DateTime time)
        {
            UserId = userId;
            Deleted = deleted;
            Time = time;
        }

    }
}
