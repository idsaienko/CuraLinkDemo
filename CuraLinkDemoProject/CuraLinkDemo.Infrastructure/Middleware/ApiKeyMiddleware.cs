using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CuraLinkDbContext db)
        {
            // Skip API key check for these paths
            var path = context.Request.Path.Value?.ToLower() ?? "";

            if (path.StartsWith("/api/residents") ||
                path.StartsWith("/api/mealschedule") ||
                path.StartsWith("/api/ausscheidung") ||
                path.StartsWith("/api/residentmovements") ||
                path.StartsWith("/api/appointments") ||
                path.StartsWith("/swagger"))
            {
                await _next(context);
                return;
            }

            // Only check API key for LLM endpoints
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            var providedHash = Security.ApiKeyGenerator.ComputeHash(providedKey!);
            var apiKey = await db.ApiKeys
                .FirstOrDefaultAsync(k => k.KeyHash == providedHash && k.IsActive);

            if (apiKey == null || (apiKey.ExpiresAt.HasValue && apiKey.ExpiresAt < DateTime.UtcNow))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid or expired API Key");
                return;
            }

            await _next(context);
        }
    }
}
