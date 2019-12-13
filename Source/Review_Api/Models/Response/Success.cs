using System;

namespace Review_Api.Models.Response
{
    public class Success
    {
        public string Message { get; set; }
        public string SuccessCode { get; set; }
        public Guid? ReviewId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Time { get; set; }

        public Success(string message, string successCode, Guid? reviewId, Guid? userid)
        {
            Message = message;
            SuccessCode = successCode;
            ReviewId = reviewId;
            UserId = userid;
            Time = DateTime.Now;
        }

        public Success(string message, string successCode, Guid? reviewId)
        {
            Message = message;
            SuccessCode = successCode;
            ReviewId = reviewId;
            UserId = null;
            Time = DateTime.Now;
        }

        public Success(string message, string successCode)
        {
            Message = message;
            SuccessCode = successCode;
            ReviewId = null;
            UserId = null;
            Time = DateTime.Now;
        }
    }
}
