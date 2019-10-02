using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class Modification
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public DateTime Time { get; set; }
        [BsonRequired]
        public string Headline { get; set; }
        [BsonRequired]
        public List<Section> Sections { get; set; }
        [BsonRequired]
        public float Rating { get; set; }

        public Modification(DateTime time, string headline, List<Section> sections, float rating)
        {
            Time = time;
            Headline = headline;
            Sections = sections;
            Rating = rating;
        }
    }
}