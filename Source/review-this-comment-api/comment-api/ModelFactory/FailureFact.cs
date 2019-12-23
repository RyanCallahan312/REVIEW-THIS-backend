using System;
using comment_api.Models.Response;

namespace comment_api.ModelFactory
{
    public class FailureFact
    {
        public static Failure Default(Exception e, Guid? userId)
        {
            return new Failure("Something went wrong", "UNSEEN_ERROR", e?.Message, userId);
        }
    }
}
