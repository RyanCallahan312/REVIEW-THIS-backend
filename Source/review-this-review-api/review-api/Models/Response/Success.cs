using System;
using System.Collections.Generic;

namespace review_api.Models.Response
{
    public class Success
    {
        public string Message { get; set; }
        public string SuccessCode { get; set; }
        public List<object> ExtraValues { get; set; }
        public DateTime Time { get; set; }

        public Success(string message, string successCode, List<object> extraValues)
        {
            Message = message;
            SuccessCode = successCode;
            ExtraValues = extraValues;
            Time = DateTime.UtcNow;
        }

        public Success(string message, string successCode)
        {
            Message = message;
            SuccessCode = successCode;
            ExtraValues = null;
            Time = DateTime.UtcNow;
        }
    }
}
