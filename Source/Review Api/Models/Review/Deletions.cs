using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
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
