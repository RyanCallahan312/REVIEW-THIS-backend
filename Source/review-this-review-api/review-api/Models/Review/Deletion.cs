using System;
using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class Deletion
    {
        [BsonRequired]
        public bool Deleted { get; }
        [BsonRequired]
        public Guid User { get; }
        [BsonRequired]
        public DateTime Time { get; }

        public Deletion(bool deleted, Guid user, DateTime time)
        {
            Deleted = deleted;
            User = user;
            Time = time;
        }
    }
}
