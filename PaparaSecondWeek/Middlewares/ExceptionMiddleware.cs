using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PaparaSecondWeek.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExeption(httpContext, ex);
            }
        }

        private Task HandleExeption(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            // Equivalent to Http status 400
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //Convert to JSON object
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return httpContext.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
