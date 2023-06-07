using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CwkBooking.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DateTimeHeader
    {
        private readonly RequestDelegate _next;

        public DateTimeHeader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.Headers.Add("my-middleware-header", DateTime.Now.ToString());
            await Task.FromResult(_next(httpContext));
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DateTimeHeaderExtensions
    {
        public static IApplicationBuilder UseDateTimeHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DateTimeHeader>();
        }
    }
}
