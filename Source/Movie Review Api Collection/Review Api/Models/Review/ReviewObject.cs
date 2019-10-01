﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Review_Api.Models
{
    public class ReviewObject
    {
        [BsonId]
        public Guid Id { get; }
        [BsonRequired]
        public string ReviewId { get; }
        [BsonRequired]
        public string UserId { get; }
        [BsonRequired]
        public DateTime Time { get; }
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
        [BsonRequired]
        public List<ModificationsObject> Modifications { get; set; }
        [BsonRequired]
        public List<DeletionsObject> Deletions { get; set; }
        [BsonRequired]
        public List<string> Comments { get; set; }

        public ReviewObject(string reviewId, string userId, DateTime time, string movie, string genre, float rating, string headline, List<SectionsObject> sections, List<ModificationsObject> modifications, List<DeletionsObject> deletions, List<string> comments)
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
            Comments = comments;
        }
    }
}
