using System;
using Review_Api.Models.Response;

namespace Review_Api.ModelFactory
{
    public class FailureFact
    {
        public static Failure BadReviewId(Exception e, Guid? userId)
        {
            return new Failure("Invalid review id", "BAD_REVIEW_ID", e?.Message, userId);
        }
        public static Failure IdNotFound(Exception e, Guid? userId)
        {
            return new Failure("ID not found", "NO_REC_BY_ID", e?.Message, userId);
        }
        public static Failure NoRecordsFound(Exception e, Guid? userId)
        {
            return new Failure("No records found for current search", "NO_REC_FOUND", e?.Message, userId);
        }
        public static Failure UnevenFilters(Exception e, Guid? userId)
        {
            return new Failure("Uneven filters", "VALUE_FIELD_COUNT_MISMATCH", e?.Message, userId);
        }
        public static Failure BadUserId(Exception e, Guid? userId)
        {
            return new Failure("Invalid user id", "BAD_USER_ID", e?.Message, userId);
        }
        public static Failure BadRequestBody(Exception e, Guid? userId)
        {
            return new Failure("Invalid request body", "BAD_REQUEST_BODY", e?.Message, userId);
        }
        public static Failure Default(Exception e, Guid? userId)
        {
            return new Failure("Something went wrong", "UNSEEN_ERROR", e?.Message, userId);
        }
    }
}
