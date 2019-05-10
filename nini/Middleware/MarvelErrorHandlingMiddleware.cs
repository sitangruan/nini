using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using nini.core.Common.ErrorHandling;
using nini.foundation.Logging;
using Newtonsoft.Json;

namespace nini.Middleware
{
    public class MarvelErrorHandlingMiddleware
    {
        private static readonly IMarvelLogger mLogger =
            MarvelLoggerFactory.Generate(typeof(MarvelErrorHandlingMiddleware));
        private readonly RequestDelegate _next;

        public MarvelErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Chain the request to next middleware
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await MarvelHandleException(context, ex);
            }
        }

        private static Task MarvelHandleException(HttpContext context, Exception ex)
        {
            MarvelError mError;
            MarvelException mException = ex as MarvelException;
            if (mException == null)
            {
                mLogger.LogError($"An unknown error occured. Message: {ex.Message}" );
                mError = new MarvelError(ex);
            }
            else
            {
                mError = mException.MarvelError;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) mError.StatusCode;
            var task = context.Response.WriteAsync(JsonConvert.SerializeObject(mError, Formatting.Indented));

            return task;
        }
    }
}
