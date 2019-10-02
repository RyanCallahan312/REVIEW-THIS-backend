using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review_Api.Models.Response
{
    public class Failure
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }

        public Failure(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
