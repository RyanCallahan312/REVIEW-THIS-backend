using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models.Review
{
    public class SubmittedReview
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public string ReviewId { get; }
        [BsonRequired]
        public string UserId { get; }
        [BsonRequired]
        public string Movie { get; }
        [BsonRequired]
        public string Genre { get; }
        [BsonRequired]
        public float Rating { get; set; }
        [BsonRequired]
        public string Headline { get; set; }
        [BsonRequired]
        public List<SectionsObject> Sections { get; set; }

        public SubmittedReview(string reviewId, string userId, string movie, string genre, float rating, string headline, List<SectionsObject> sections)
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
