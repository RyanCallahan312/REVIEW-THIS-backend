using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class SubmittedReview
    {
        [BsonId]
        public Guid ReviewId { get; }
        [BsonRequired]
        public Guid UserId { get; }
        [BsonRequired]
        public string Movie { get; }
        [BsonRequired]
        public string Genre { get; }
        [BsonRequired]
        public float Rating { get; }
        [BsonRequired]
        public string Headline { get; }
        [BsonRequired]
        public List<Section> Sections { get; }

        public SubmittedReview(Guid reviewId, Guid userId, string movie, string genre, float rating, string headline, List<Section> sections)
        {
            ReviewId = reviewId;
            UserId = userId;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
            Sections = sections;
        }
    }
}
