using comment_api.Models.Response;
using System;
using System.Collections.Generic;

namespace comment_api.ModelFactory
{
    public static class SuccessFact
    {
        public static Success CommentCreated(Guid? commentId, Guid? userId)
        {
            return new Success("Comment created successfully", "COMMENT_CREATED", new List<object>() { commentId, userId });
        }
        public static Success CommentsByReviewRetrieved(Guid? reviewId, Guid? userId)
        {
            return new Success("Comments successfully retrieved", "COMMENTS_BY_REVIEW_RETRIEVED", new List<object>() { reviewId, userId });
        }
        public static Success CommentRetrieved(Guid? commentId, Guid? userId)
        {
            return new Success("Comment successfully retrieved", "COMMENT_RETRIEVED", new List<object>() { commentId, userId });
        }
        public static Success CommentSoftDelete(Guid? commentId, Guid? userId)
        {
            return new Success("Comment successfully deleted", "COMMENT_SOFT_DELETED", new List<object>() { commentId, userId });
        }
        public static Success CommentReinstated(Guid? commentId, Guid? userId)
        {
            return new Success("Comment successfully reinstated", "COMMENT_REINSTATED", new List<object>() { commentId, userId });
        }
        public static Success Default(Guid? commentId, Guid? userId)
        {
            return new Success("Completed operation", "REQUEST_FUFILLED", new List<object>() { commentId, userId });
        }
    }
}
