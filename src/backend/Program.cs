using backend.Application.Service;
using backend.Infrastructure.Context;
using backend.Infrastructure.Repository;
using Marten;
using Scalar.AspNetCore;
using Serilog;
using Weasel.Core;

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


// This is the absolute, simplest way to integrate Marten into your
// .NET application with Marten's default configuration
builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("MartenConnectionString") ?? "Host=localhost;Database=postgres;Username=postgres;Password=postgres");

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization();

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.AddSingleton<MartenContext>();
builder.Services.AddScoped<BikeRepository>();
builder.Services.AddScoped<BikeService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
//TODO: Uncomment the following line to disable the OpenAPI UI in production mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
