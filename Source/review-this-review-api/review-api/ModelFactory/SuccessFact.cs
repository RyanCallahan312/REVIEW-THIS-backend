using review_api.Models;
using review_api.Models.Query;
using review_api.Models.Response;
using System;
using System.Collections.Generic;

namespace review_api.ModelFactory
{
    public static class SuccessFact
    {
        public static Success ReviewCreated(Guid? reviewId, Guid? userId)
        {
            return new Success("Review created successfully", "REVIEW_CREATED", new List<object>() { reviewId, userId });
        }
        public static Success ReviewsRetrieved(Guid? userId, Sort sort, List<Filter> filters, Page page)
        {
            return new Success("Many reviews successfully retrieved", "MANY_PARTIAL_REVEWS_RETRIEVED", new List<object>() { userId, sort, filters, page });
        }
        public static Success ReviewRetrieved(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully retrieved", "SINGLE_COMPLETE_REVIEW_RETRIEVED", new List<object>() { reviewId, userId });
        }
        public static Success ReviewSoftDelete(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully deleted", "REVIEW_SOFT_DELETED", new List<object>() { reviewId, userId });
        }
        public static Success ReviewReinstated(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully reinstated", "REVIEW_REINSTATED", new List<object>() { reviewId, userId });
        }
        public static Success ReviewSectionsModified(Guid? reviewId, Guid? userId, List<Section> sections)
        {
            return new Success("Review sections successfully modified", "REVIEW_SECTION_MODIFICATION", new List<object>() { reviewId, userId, sections });
        }
        public static Success Default(Guid? reviewId, Guid? userId)
        {
            return new Success("Completed operation", "REQUEST_FUFILLED", new List<object>() { reviewId, userId });
        }
    }
}
