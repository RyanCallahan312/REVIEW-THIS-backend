using Review_Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.ModelFactory
{
    public class SuccessFact
    {
        public static Success ReviewCreated(Guid id)
        {
            return new Success("Review created successfully", "REVIEW_CREATED", id);
        }
        public static Success AllReviewsRetrieved()
        {
            return new Success("All reviews successfully retrieved", "ALL_PARTIAL_REVEWS_RETRIEVED");
        }
        public static Success ReviewRetrieved(Guid id)
        {
            return new Success("Review Retrieved", "SINGLE_COMPLETE_REVIEW_RETRIEVED", id);
        }
        public static Success Default()
        {
            return new Success("Completed operation", "REQUEST_FUFILLED");
        }
    }
}
