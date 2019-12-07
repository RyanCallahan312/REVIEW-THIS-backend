using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models.Response
{
    public class Unauthorized
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Time { get; set; }

        public Unauthorized(string message, string errorCode, Guid? userId)
        {
            Message = message;
            ErrorCode = errorCode;
            UserId = userId;
            Time = DateTime.Now;
        }

        public Unauthorized(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
            UserId = null;
            Time = DateTime.Now;
        }
    }
}
