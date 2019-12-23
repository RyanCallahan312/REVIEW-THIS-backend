using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class Modification 
    {
        [BsonRequired]
        public DateTime Time { get; set; }
        [BsonRequired]
        public Guid User { get; set; }
        [BsonRequired]
        public string Headline { get; set; }
        [BsonRequired]
        public List<Section> Sections { get; set; }
        [BsonRequired]
        public float Rating { get; set; }
        [BsonRequired]
        public List<Guid> Comments { get; set; }

        public Modification(DateTime time, Guid user, string headline, List<Section> sections, float rating, List<Guid> comments)
        {
            Time = time;
            User = user;
            Headline = headline;
            Sections = sections;
            Rating = rating;
            Comments = comments;
        }
    }
}