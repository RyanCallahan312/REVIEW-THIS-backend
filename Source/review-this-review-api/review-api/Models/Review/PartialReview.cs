﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace review_api.Models
{
    public class PartialReview
    {
        [BsonId]
        public Guid ReviewId { get; }
        [BsonRequired]
        public string Author { get; }
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


        public PartialReview(Guid reviewId, Guid userId, string author, string movie, string genre, float rating, string headline)
        {
            ReviewId = reviewId;
            UserId = userId;
            Author = author;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
        }

        public PartialReview(Review fullReview)
        {
            ReviewId = fullReview.ReviewId;
            Author = fullReview.Author;
            UserId = fullReview.UserId;
            Movie = fullReview.Movie;
            Genre = fullReview.Genre;
            Rating = fullReview.Rating;
            Headline = fullReview.Headline;
        }
    }
}
