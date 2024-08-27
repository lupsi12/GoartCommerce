using Application;
using Domain;
using Core;  // Add the Core namespace
using Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var authDbConnectionString = Environment.GetEnvironmentVariable("authdb_connectionstring");

if (string.IsNullOrEmpty(authDbConnectionString))
{
    throw new InvalidOperationException("The environment variable 'authdb_connectionstring' is not set.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(authDbConnectionString, ServerVersion.AutoDetect(authDbConnectionString)));

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
