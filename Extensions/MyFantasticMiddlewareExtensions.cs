using APBD8.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace APBD8.Extensions
{
    public static class MyFantasticMiddlewareExtensions
    {

        public static IApplicationBuilder UseMyFantasticErrorLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyFantasticErrorLoggingMiddleware>();
        }
    }
}
