
using Application;
using Core;
using Domain;
using MongoDB.Driver; // Add the Core namespace

var builder = WebApplication.CreateBuilder(args);

var homeDbConnectionString = Environment.GetEnvironmentVariable("homedb_connectionstring");

if (string.IsNullOrEmpty(homeDbConnectionString))
{
    throw new InvalidOperationException("The environment variable 'homedb_connectionstring' is not set.");
}

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySql(productDbConnectionString, ServerVersion.AutoDetect(productDbConnectionString)));
//
// MongoDB client ve database ayarlarını DI'ye ekleyin
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(homeDbConnectionString));
builder.Services.AddScoped<IMongoDatabase>(s => s.GetRequiredService<IMongoClient>().GetDatabase("homedb")); // Veritabanı adını belirtin

builder.Services.AddDomainLayerServices();
//
builder.Services.AddApplicationLayerServices(builder.Configuration);

//builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(homeDbConnectionString));

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