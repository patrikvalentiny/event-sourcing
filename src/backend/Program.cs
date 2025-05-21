using backend.Application.Service;
using backend.Infrastructure.Aggregations;
using backend.Infrastructure.Context;
using backend.Infrastructure.Repository;
using Marten;
using Marten.Events;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Marten.Events.Schema;
using Scalar.AspNetCore;
using Serilog;
using Weasel.Core;
using Wolverine;

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger = log;
Log.Information("The global logger has been configured");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();





builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("MartenConnectionString") ?? "Host=localhost;Database=postgres;Username=postgres;Password=postgres");

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization();

    // Lighter weight mode that should result in better
    // performance, but with a loss of available metadata
    // within inline projections
    options.Events.AppendMode = EventAppendMode.Quick;
    options.Events.UseMandatoryStreamTypeDeclaration = true;

    options.Projections.Add<BikeAggregation>(ProjectionLifecycle.Inline);

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.AddSingleton<MartenContext>();
builder.Services.AddScoped<BikeRepository>();
builder.Services.AddScoped<RideRepository>();

builder.Host.UseWolverine(options =>
{
    options.Durability.Mode = DurabilityMode.MediatorOnly;
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
