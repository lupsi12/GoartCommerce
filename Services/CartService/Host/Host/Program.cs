using Application;
using Domain;
using Core;  // Add the Core namespace
using Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from the environment variable
var cartDbConnectionString = Environment.GetEnvironmentVariable("CARTDB_CONNECTIONSTRING");

if (string.IsNullOrEmpty(cartDbConnectionString))
{
    throw new InvalidOperationException("The environment variable 'CARTDB_CONNECTIONSTRING' is not set.");
}

// Configure the PostgreSQL database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(cartDbConnectionString));  // Use PostgreSQL instead of MySQL

builder.Services.AddDomainLayerServices();
builder.Services.AddApplicationLayerServices(builder.Configuration);
builder.Services.AddCoreLayerServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
