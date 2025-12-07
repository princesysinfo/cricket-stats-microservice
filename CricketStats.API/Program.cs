using CricketStats.Application.Interface;
using CricketStats.Domain.Entities;
using CricketStats.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/health", () =>
{
    return Results.Ok(new { status = "healthy", time = DateTime.UtcNow });
})
.WithName("HealthCheck");

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
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/sampleplayer", (IPlayerService playerService) =>
{
    var player = playerService.GetSamplePlayer();
    return Results.Ok(player);
})
.WithName("GetSamplePlayer");

app.MapPost("/players",(Player player, IPlayerRepository repo) => { 
var created = repo.Add(player);
    return Results.Created($"/players/{created.Id}", created);
});

app.MapGet("/players", (IPlayerRepository repo) =>
{   
    var Getall = repo.GetAll();
    return Results.Ok(Getall);
});

app.MapGet("/Players/{id}", (int Id, IPlayerRepository repo) =>
{
    var GetById = repo.GetById(Id);
    return Results.Ok(GetById);
});

app.MapPut("/Players/{id}", (int Id, Player updatePlayer, IPlayerRepository repo) =>
{
    updatePlayer.Id = Id;
    var updated = repo.Update(updatePlayer);
    return updated is null? Results.NotFound(): Results.Ok(updated);
});

app.MapDelete("/Players/{id}", (int Id, IPlayerRepository repo) =>
{
    var deleted = repo.Delete(Id);
    return deleted ? Results.NoContent() : Results.NotFound();
});
app.Run();
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


