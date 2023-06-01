using App;
using Domain;
using Domain.MeterReadings;
using Domain.Services.CsvMeterReadings;
using Domain.Web;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Persistence.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddMediatR(config
    => config.RegisterServicesFromAssemblies(
        typeof(MeterReadingsHandler).Assembly,
        typeof(FileInputHandler).Assembly));

builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddTransient<IMeterReadingsInput, CsvMeterReadingsInput>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/meter-reading-uploads", RestMappings.Import);

app.MapHealthChecks("/health");

app.Run();
