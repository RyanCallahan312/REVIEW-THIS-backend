using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Review_Api.Models
{
    public class Review
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
        public float Rating { get { return Rating; } set { Rating = value; Modifications.Add(new Modification(DateTime.Now, Headline, Sections, Rating)); } }
        [BsonRequired]
        public string Headline { get { return Headline; } set { Headline = value; Modifications.Add(new Modification(DateTime.Now, Headline, Sections, Rating)); } }
        [BsonRequired]
        public List<Section> Sections { get { return Sections; } set { Sections = value; Modifications.Add(new Modification(DateTime.Now, Headline, Sections, Rating)); } }
        [BsonRequired]
        public List<Modification> Modifications { get; set; }
        [BsonRequired]
        public List<Deletion> Deletions { get; }
        [BsonRequired]
        public List<Guid> Comments { get; }

        public Review(string reviewId, string userId, DateTime time, string movie, string genre, float rating, string headline, List<Section> sections, List<Modification> modifications, List<Deletion> deletions, List<Guid> comments)
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

        public Review(string reviewId, string userId, string movie, string genre, float rating, string headline, List<Section> sections)
        {

            ReviewId = reviewId;
            UserId = userId;
            Time = DateTime.Now;
            Movie = movie;
            Genre = genre;
            Rating = rating;
            Headline = headline;
            Sections = sections;
            Deletions = null;
            Comments = null;
            Modifications = new List<Modification> { new Modification(Time, Headline, Sections, Rating) };
        }




    }
}
