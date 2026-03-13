// Middleware/RequestTrackingMiddleware.cs
using System.Diagnostics;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Middleware
{
    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestLogService logService)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var log = new RequestLog
            {
                Url = context.Request.Path,
                ExecutionTimeMs = stopwatch.ElapsedMilliseconds,
                Time = DateTime.Now
            };

            logService.AddLog(log);
        }
    }
}