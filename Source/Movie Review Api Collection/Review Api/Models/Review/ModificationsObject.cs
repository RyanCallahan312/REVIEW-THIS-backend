using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class ModificationsObject
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public DateTime Time { get; set; }
        [BsonRequired]
        public string Headline { get; set; }
        [BsonRequired]
        public List<SectionsObject> Sections { get; set; }
        [BsonRequired]
        public float Rating { get; set; }

        public ModificationsObject(DateTime time, string headline, List<SectionsObject> sections, float rating)
        {
            Time = time;
            Headline = headline;
            Sections = sections;
            Rating = rating;
        }
    }
}