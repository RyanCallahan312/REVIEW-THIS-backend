using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class Section
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public string Header { get; set; }
        [BsonRequired]
        public string Body { get; set; }

        public Section(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}
