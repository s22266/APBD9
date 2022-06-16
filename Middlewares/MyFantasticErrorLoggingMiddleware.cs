using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APBD8.Middlewares
{
    public class MyFantasticErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public MyFantasticErrorLoggingMiddleware(RequestDelegate next) 
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                StreamWriter streamWriter = new("log.txt", append: true);
                await streamWriter.WriteLineAsync($"{DateTime.Now} - {e.Message}");
            }
        }
    }
}
