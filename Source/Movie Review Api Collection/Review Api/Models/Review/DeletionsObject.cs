using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class DeletionsObject
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public bool Deleted { get; set; }
        [BsonRequired]
        public string User { get; set; }
        [BsonRequired]
        public DateTime Time { get; set; }

        public DeletionsObject(bool deleted, string user, DateTime time)
        {
            Deleted = deleted;
            User = user;
            Time = time;
        }
    }
}
