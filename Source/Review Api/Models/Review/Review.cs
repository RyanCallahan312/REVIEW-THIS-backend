﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Review_Api.Models
{
    public class Review
    {
        [BsonId]
        public Guid ReviewId { get; set; }
        [BsonRequired]
        public Guid UserId { get; set; }
        [BsonRequired]
        public DateTime Time { get; set; }
        [BsonRequired]
        public string Movie { get; set; }
        [BsonRequired]
        public string Genre { get; set; }
        [BsonRequired]
        public float Rating { get; set; }
        [BsonRequired]
        public string Headline { get; set; }
        [BsonRequired]
        public List<Section> Sections { get; set; }
        [BsonRequired]
        public List<Modification> Modifications { get; set; }
        [BsonRequired]
        public List<Deletion> Deletions { get; set; }
        [BsonRequired]
        public bool Deleted { get; set; }
        [BsonRequired]
        public List<Guid> Comments { get; set; }

        public Review(Guid reviewId, Guid userId, DateTime time, string movie, string genre, float rating, string headline, List<Section> sections, List<Modification> modifications, List<Deletion> deletions, bool deleted, List<Guid> comments)
        {
            ReviewId = reviewId;
            UserId = userId;
            Time = time;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
            Sections = sections;
            Modifications = modifications;
            Deletions = deletions;
            Deleted = deleted;
            Comments = comments;
        }
        [JsonConstructor]
        public Review(Guid userId, string movie, string genre, float rating, string headline, List<Section> sections)
        {

            ReviewId = Guid.NewGuid();
            UserId = userId;
            Time = DateTime.Now;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
            Sections = sections;
            Deletions = new List<Deletion>()
            {
                new Deletion(false, userId, DateTime.Now)
            };
            Deleted = false;
            Comments = new List<Guid>();
            Modifications = new List<Modification> { new Modification(Time, userId, Headline, Sections, Rating) };
        }

        public void Delete(Guid userId)
        {
            Deletions.Add(new Deletion(true, userId, DateTime.Now));
            Deleted = true;
        }

        public void ReInstate(Guid userId)
        {
            Deletions.Add(new Deletion(false, userId, DateTime.Now));
            Deleted = false;
        }

        public void SetRating(float value, Guid userId)
        {
            Rating = value;
            Modifications.Add(new Modification(DateTime.Now, userId, Headline, Sections, Rating));
        }

        public void SetHeadline(string value, Guid userId)
        {
            Headline = value;
            Modifications.Add(new Modification(DateTime.Now, userId, Headline, Sections, Rating));
        }

        public void SetSection(List<Section> value, Guid userId)
        {
            Sections = value;
            Modifications.Add(new Modification(DateTime.Now, userId, Headline, Sections, Rating));
        }

        public PartialReview ToPartialReview()
        {
            return new PartialReview(this);
        }
    }
}