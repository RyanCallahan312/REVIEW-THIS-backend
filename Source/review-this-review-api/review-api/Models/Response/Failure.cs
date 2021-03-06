﻿using System;
using System.Collections.Generic;

namespace review_api.Models.Response
{
    public class Failure
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string Exception { get; set; }
        public DateTime Time { get; set; }
        public List<object> ExtraValues { get; set; }

        public Failure(string message, string errorCode, string exception, List<object> extraValues)
        {
            Message = message;
            ErrorCode = errorCode;
            Exception = exception;
            ExtraValues = extraValues;
            Time = DateTime.UtcNow;
        }
        public Failure(string message, string errorCode, string exception)
        {
            Message = message;
            ErrorCode = errorCode;
            Exception = exception;
            ExtraValues = null;
            Time = DateTime.UtcNow;
        }
    }
}
