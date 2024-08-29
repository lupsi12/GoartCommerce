using Domain;
using Core;
using Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application;

var builder = WebApplication.CreateBuilder(args);

var orderDbConnectionString = Environment.GetEnvironmentVariable("orderdb_connectionstring");

if (string.IsNullOrEmpty(orderDbConnectionString))
{
    throw new InvalidOperationException("The environment variable 'orderdb_connectionstring' is not set.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(orderDbConnectionString));
builder.Services.AddDomainLayerServices();

builder.Services.AddApplicationLayerServices(builder.Configuration);

builder.Services.AddCoreLayerServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
