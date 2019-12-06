using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models.Response
{
    public class Success
    {
        public string Message { get; set; }
        public string SuccessCode { get; set; }
        public Guid? ReviewId { get; set; }

        public Success(string message, string successCode, Guid reviewId)
        {
            Message = message;
            SuccessCode = successCode;
            ReviewId = reviewId;
        }

        public Success(string message, string successCode)
        { 
            Message = message;
            SuccessCode = successCode;
            ReviewId = null;
        }
    }
}
