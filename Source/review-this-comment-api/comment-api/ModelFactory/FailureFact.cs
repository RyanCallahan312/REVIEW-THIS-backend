using System;
using System.Collections.Generic;
using comment_api.Models.Response;
using review_api.Models.Query;

namespace comment_api.ModelFactory
{
    public static class FailureFact
    {
        public static Failure BadCommentId(Exception e, Guid? userId, Guid commentId)
        {
            return new Failure("Invalid comment id", "BAD_COMMENT_ID", e?.Message, new List<object> { userId, commentId });
        }
        public static Failure IdNotFound(Exception e, Guid? userId, Guid commentId)
        {
            return new Failure("ID not found", "NO_REC_BY_ID", e?.Message, new List<object> { userId, commentId });
        }
        public static Failure NoRecordsFound(Exception e, Guid? userId, Sort sort, List<Filter> filters, Page page)
        {
            return new Failure("No records found for current search", "NO_REC_FOUND", e?.Message, new List<object> { userId, sort, filters, page });
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
