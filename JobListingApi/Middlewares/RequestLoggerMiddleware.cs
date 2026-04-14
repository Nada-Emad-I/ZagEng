using System.Diagnostics;

namespace JobListingsAPI.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;
            var sw = Stopwatch.StartNew();

            await _next(context);

            
            sw.Stop();

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var statusCode = context.Response.StatusCode;
            var statusText = GetStatusText(statusCode);
            var elapsed = sw.ElapsedMilliseconds;

            Console.WriteLine(
                $"[{timestamp}] {method} {path} → {statusCode} {statusText}  (took {elapsed}ms)");
        }

        private static string GetStatusText(int code) => code switch
        {
            200 => "OK",
            201 => "Created",
            204 => "No Content",
            400 => "Bad Request",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => string.Empty
        };
    }
}
