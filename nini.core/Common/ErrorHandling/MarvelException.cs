using System;
using System.Net;

namespace nini.core.Common.ErrorHandling
{
    public class MarvelException: Exception
    {
        public MarvelError MarvelError { get; private set; }

        public MarvelException(HttpStatusCode statusCode, string message, Exception ex)
        {
            string stackTrace = ex?.StackTrace ?? Environment.StackTrace;
            MarvelError = new MarvelError(statusCode, message, stackTrace);
        }
    }
}
