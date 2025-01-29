using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Services;
using Entities;

namespace MyShop
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext,IRatingService ratingservice)
        {
            Rating rating = new()
            {   Host=httpContext.Request.Host.ToString(),
                Method = httpContext.Request.Method.ToString(),
                Path=httpContext.Request.Path.ToString(),
                Referer=httpContext.Request.Headers.Referer.ToString(),
                UserAgent=httpContext.Request.Headers.UserAgent.ToString(),
                RecordDate=DateTime.Now
            };
            ratingservice.addRating(rating);

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
