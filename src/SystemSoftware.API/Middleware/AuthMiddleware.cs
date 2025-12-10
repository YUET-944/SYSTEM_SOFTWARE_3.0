using SystemSoftware.Core.Data;
using SystemSoftware.Core.Models;
using SystemSoftware.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace SystemSoftware.API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuthMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authentication for auth endpoints and root
            if (context.Request.Path.StartsWithSegments("/api/auth") || 
                context.Request.Path == "/")
            {
                await _next(context);
                return;
            }

            // Get token from header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = authHeader?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Authorization token required" }));
                return;
            }

            // Validate JWT token
            using var scope = _serviceProvider.CreateScope();
            var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            var principal = jwtService.ValidateToken(token);
            if (principal == null)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Invalid or expired token" }));
                return;
            }

            // Extract user information from token
            var userIdClaim = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var storeIdClaim = principal.FindFirst("StoreId");
            
            if (userIdClaim == null || storeIdClaim == null)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Invalid token claims" }));
                return;
            }

            if (!int.TryParse(userIdClaim.Value, out int userId) || !int.TryParse(storeIdClaim.Value, out int storeId))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Invalid token format" }));
                return;
            }

            var user = await dbContext.Users
                .Include(u => u.Store)
                .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);

            if (user == null)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Invalid or inactive user" }));
                return;
            }

            // Add user and store to context items for controllers to use
            context.Items["User"] = user;
            context.Items["StoreId"] = user.StoreId;
            context.Items["UserPrincipal"] = principal;

            await _next(context);
        }
    }
}
