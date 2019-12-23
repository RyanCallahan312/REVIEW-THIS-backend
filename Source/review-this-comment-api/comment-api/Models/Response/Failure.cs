using System;

namespace comment_api.Models.Response
{
    public class Failure
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string Exception { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Time { get; set; }
        public Failure(string message, string errorCode, string exception, Guid? userId)
        {
            Message = message;
            ErrorCode = errorCode;
            Exception = exception;
            UserId = userId;
            Time = DateTime.Now;
        }
        public Failure(string message, string errorCode, string exception)
        {
            Message = message;
            ErrorCode = errorCode;
            Exception = exception;
            UserId = null;
            Time = DateTime.Now;
        }
    }
}
