using Review_Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.ModelFactory
{
    public class SuccessFact
    {
        public static Success ReviewCreated(Guid? reviewId, Guid? userId)
        {
            return new Success("Review created successfully", "REVIEW_CREATED", reviewId, userId);
        }
        public static Success AllReviewsRetrieved(Guid? userId)
        {
            return new Success("All reviews successfully retrieved", "ALL_PARTIAL_REVEWS_RETRIEVED", null, userId);
        }
        public static Success ReviewRetrieved(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully retrieved", "SINGLE_COMPLETE_REVIEW_RETRIEVED", reviewId, userId);
        }
        public static Success ReviewSoftDelete(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully deleted", "REVIEW_SOFT_DELETED", reviewId, userId);
        }
        public static Success ReviewReinstated(Guid? reviewId, Guid? userId)
        {
            return new Success("Review successfully reinstated", "REVIEW_REINSTATED", reviewId, userId);
        }
        public static Success ReviewSectionsModified(Guid? reviewId, Guid? userId)
        {
            return new Success("Review sections successfully modified", "REVIEW_SECTION_MODIFICATION", reviewId, userId);
        }
        public static Success Default()
        {
            return new Success("Completed operation", "REQUEST_FUFILLED");
        }
    }
}
