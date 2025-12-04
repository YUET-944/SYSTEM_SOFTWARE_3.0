using SystemSoftware.Core.Data;
using SystemSoftware.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication Service
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

// Add root endpoint with navigation
app.MapGet("/", () => new
{
    message = "Universal Business System API",
    version = "1.0.0",
    navigation = new
    {
        endpoints = new[]
        {
            new { url = "/api/home", text = "Home", method = "GET" },
            new { url = "/api/system", text = "System", method = "GET" },
            new { url = "/api/modules", text = "Modules", method = "GET" },
            new { url = "/api/auth/register", text = "Register", method = "POST" },
            new { url = "/api/auth/login", text = "Login", method = "POST" },
            new { url = "/weatherforecast", text = "Weather Forecast", method = "GET" }
        }
    }
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    
    return new
    {
        data = forecast,
        navigation = new
        {
            back = new
            {
                url = "/api/home",
                text = "← Back to Home"
            },
            current = "Weather Forecast"
        }
    };
})
.WithName("GetWeatherForecast");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
