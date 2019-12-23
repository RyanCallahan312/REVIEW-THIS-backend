using System;
using System.Collections.Generic;
using comment_api.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace comment_api.Models
{
    public class Comment
    {
        [BsonId]
        public Guid CommentId { get; }
        [BsonRequired]
        public Guid UserId { get; }
        [BsonRequired]
        public Guid ParentId { get; }
        [BsonRequired]
        public Guid ReviewId { get; }
        [BsonRequired]
        public DateTime Time { get; }
        [BsonRequired]
        public List<Modification> Modifications { get; set; }
        [BsonRequired]
        public bool Deleted { get; set; }
        [BsonRequired]
        public List<Deletion> Deletions { get; set; }
        [BsonRequired]
        public string Body { get; set; }

        public Comment( Guid commentId, Guid userId, Guid parentId, DateTime time, List<Modification> modifications, bool deleted, List<Deletion> deletions, string body)
        {
            CommentId = commentId;
            UserId = userId;
            ParentId = parentId;
            Time = time;
            Modifications = modifications;
            Deleted = deleted;
            Deletions = deletions;
            Body = body;
        }

        public Comment(Guid userId, Guid parentId, string body)
        {
            CommentId = Guid.NewGuid();
            UserId = userId;
            ParentId = parentId;
            Time = DateTime.Now;
            Modifications = new List<Modification>();
            Deleted = false;
            Deletions = new List<Deletion>();
            Body = body;
        }

        public void Delete(Guid userId)
        {
            Deleted = true;
            Deletions.Add(new Deletion(userId, Deleted, DateTime.Now));
        }

        public void Reinstate(Guid userId)
        {
            Deleted = false;
            Deletions.Add(new Deletion(userId, Deleted, DateTime.Now));
        }

        public void SetBody(string body, Guid userId)
        {
            Modifications.Add(new Modification(Time, Body, userId));
            Body = body;
        }

    }
}
