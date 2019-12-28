using System;
using System.Collections.Generic;
using review_api.Models.Query;
using review_api.Models.Response;

namespace review_api.ModelFactory
{
    public static class FailureFact
    {
        public static Failure BadReviewId(Exception e, Guid? userId, Guid reviewId)
        {
            return new Failure("Invalid review id", "BAD_REVIEW_ID", e?.Message, new List<object>() { userId, reviewId } ) ;
        }
        public static Failure IdNotFound(Exception e, Guid? userId, Guid reviewId)
        {
            return new Failure("ID not found", "NO_REC_BY_ID", e?.Message, new List<object>() { userId, reviewId });
        }
        public static Failure NoRecordsFound(Exception e, Guid? userId, Sort sort, List<Filter> filter, Page page)
        {

            return new Failure("No records found for current search", "NO_REC_FOUND", e?.Message, new List<object>() { userId, sort, filter, page });
        }
        public static Failure UnevenFilters(Exception e, Guid? userId, string values, string fields)
        {
            return new Failure("Uneven filters", "VALUE_FIELD_COUNT_MISMATCH", e?.Message, new List<object>() { userId, values, fields });
        }
        public static Failure BadUserId(Exception e, dynamic userId)
        {
            return new Failure("Invalid user id", "BAD_USER_ID", e?.Message, new List<object>() { userId });
        }
        public static Failure BadRequestBody(Exception e, Guid? userId, dynamic requestBody)
        {
            return new Failure("Invalid request body", "BAD_REQUEST_BODY", e?.Message, new List<object> { userId, requestBody });
        }
        public static Failure Default(Exception e, Guid? userId)
        {
            return new Failure("Something went wrong", "UNSEEN_ERROR", e?.Message, new List<object>() { userId });
        }
    }
}
