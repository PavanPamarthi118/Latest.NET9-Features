using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi(); // Enables OpenAPI (Swagger UI) support
builder.Services.AddMemoryCache(); // Registers in-memory caching

// HybridCache is a new feature in .NET 9 that combines memory and distributed caching.
#pragma warning disable EXTEXP0018 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
builder.Services.AddHybridCache();
#pragma warning restore EXTEXP0018 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var app = builder.Build();

// Configure OpenAPI only in development mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection(); // Enforce HTTPS

// Sample weather descriptions
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

/*
=============================================
  BEFORE: Using IMemoryCache (Old Approach) 
=============================================

app.MapGet("/weatherforecast", (string city, IMemoryCache memoryCache) =>
{
    // Try to get the cached value
    if (!memoryCache.TryGetValue(city, out WeatherForecast[]? response))
    {
        // If not found, generate new weather data
        response = CreateResponse();
        
        // Store in memory cache for 5 minutes
        memoryCache.Set(city, response, TimeSpan.FromMinutes(5));
    }

    return Results.Ok(response);
});

*/


/*  
=============================================
 NOW: Using HybridCache in .NET 9 (New Approach)
=============================================
*/

app.MapGet("/weatherforecast", async (string city, HybridCache hybridCache, CancellationToken cancellationToken) =>
{
    // Correct usage of GetOrCreateAsync with explicit types and proper argument order
    var response = await hybridCache.GetOrCreateAsync(
        city,                          // Cache key (city name)
        city,                          // State (we just reuse the city name)
        (city, ct) => new ValueTask<WeatherForecast[]>(CreateResponse(city)), // Factory method
        new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(5) // Cache expiration time
        },
        cancellationToken: cancellationToken // Pass CancellationToken
    );

    return Results.Ok(response);
});

// Generates a random weather forecast
WeatherForecast[] CreateResponse(string city)
{
    return Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Generate future dates
            Random.Shared.Next(-20, 55),                        // Random temperature
            summaries[Random.Shared.Next(summaries.Length)]      // Random summary
        ))
        .ToArray();
}

app.Run();

// Weather forecast record with computed Fahrenheit conversion
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


/*Caching is a technique to store frequently accessed data temporarily in a way that makes it faster to retrieve,
reducing the need for expensive database queries or API calls

1. Memory Cache(Local In-Memory Caching) - Memory caching stores data inside the application’s RAM.
It is local to a single instance of the application and provides super-fast access because everything is stored in memory

---> Best for (Single-instance apps)
---> Data Lost on Restart? (✅ Yes)
---> Works Across Multiple Servers? (❌ No)
---> Speed (⚡ Fastest)

2. Distributed Cache(External Storage for Scaling) - A distributed cache stores data in an external system (like Redis, SQL Server, or a cloud-based service).
Unlike memory cache, data is shared between multiple instances of an application
---> Best for (Scalable, cloud-based apps)
---> Data Lost on Restart? (❌ No)
---> Works Across Multiple Servers? (✅ Yes)
---> Speed (⚡⚡ Fastest)

3. Hybrid Cache(Best of Both Worlds) => Memory + Distributed - Hybrid cache combines the speed of memory cache with the reliability of distributed cache.
It keeps hot data in memory for fast access but stores data in distributed cache to make it available across multiple servers.
---> Best for (High-performance, scalable apps)
---> Data Lost on Restart? (❌ No)
---> Works Across Multiple Servers? (✅ Yes)
---> Speed (⚡⚡⚡ Fastest)*/