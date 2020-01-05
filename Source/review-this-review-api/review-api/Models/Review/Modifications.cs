using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class Modification 
    {
        [BsonRequired]
        public DateTime Time { get;}
        [BsonRequired]
        public Guid User { get;}
        [BsonRequired]
        public string Headline { get;}
        [BsonRequired]
        public List<Section> Sections { get;}
        [BsonRequired]
        public float Rating { get;}
        [BsonRequired]
        public List<Guid> Comments { get;}

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