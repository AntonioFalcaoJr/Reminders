using Application.DependencyInjection;
using CorrelationId;
using Infrastructure.EventBus.DependencyInjection.Extensions;
using Infrastructure.EventStore.DependencyInjection.Extensions;
using Serilog;
using WebAPI.APIs;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureLogging()
    .ConfigureServiceProvider()
    .ConfigureAppConfiguration();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policyBuilder
        => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services
    .AddCorrelationId()
    .AddProblemDetails();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services
    .AddApplication()
    .AddEventStore()
    .AddEventBus();

builder.Services.AddHealthChecks();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();
app.UseCorrelationId();
app.UseSerilogRequestLogging();

app.MapHealthChecks("/healthz");
app.MapRemindersApi();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        await app.MigrateEventStoreAsync();

    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}