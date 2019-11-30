using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review_Api.Models
{
    public class PartialReview
    {
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


        public PartialReview(string reviewId, string userId, string movie, string genre, float rating, string headline)
        {
            ReviewId = reviewId;
            UserId = userId;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
        }

        public PartialReview(Review fullReview)
        {
            ReviewId = fullReview.ReviewId;
            UserId = fullReview.UserId;
            Movie = fullReview.Movie;
            Genre = fullReview.Genre;
            Rating = fullReview.Rating;
            Headline = fullReview.Headline;
        }
    }
}
