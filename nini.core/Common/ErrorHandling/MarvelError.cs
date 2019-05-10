using System;
using System.Net;

namespace nini.core.Common.ErrorHandling
{
    public class MarvelError
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string Message { get; internal set; }

        protected internal string StackTrace { get; internal set; }

        public MarvelError(Exception ex) : this(HttpStatusCode.InternalServerError, ex.Message,
            ex.StackTrace)
        {

        }

        internal MarvelError(HttpStatusCode statusCode, string message, string stackTrace)
        {
            StatusCode = statusCode;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}
