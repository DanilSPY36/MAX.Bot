using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MAX.Bot.Exceptions
{
    public class MaxRequestException : Exception
    {
        public HttpStatusCode? StatusCode { get; }
        public MaxErrorCode? ErrorCode { get; }

        public MaxRequestException(string message): base(message) { }
        public MaxRequestException(string message, Exception? inner = null) : base(message, inner) { }
        public MaxRequestException(string message, HttpStatusCode? statusCode = null) : base(message) { StatusCode = statusCode; }
        public MaxRequestException(string message, HttpStatusCode? statusCode = null, Exception? inner = null) : base(message, inner) { StatusCode = statusCode; }
        public MaxRequestException(string message, HttpStatusCode? statusCode = null, Exception? inner = null, MaxErrorCode? errorCode = null): base(message, inner)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
