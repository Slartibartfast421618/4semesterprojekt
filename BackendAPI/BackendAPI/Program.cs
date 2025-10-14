using BackendAPI.Data;
using BackendAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddControllers();
// Scalar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins",
        policy =>
        {
            policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
        });
});
// Entity Framework connection to DB
builder.Services.AddDbContext<MaMaDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Nominatim 
builder.Services.AddHttpClient<NominatimGeocodingService>("NominatimAPI", (sp, client) =>
{
    client.BaseAddress = new Uri(builder.Configuration["Nominatim:BaseUrl"] ?? "https://nominatim.openstreetmap.org/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "ManeManager");
});
builder.Services.AddScoped<IGeocodingService, NominatimGeocodingService>();
// Treatment service
builder.Services.AddScoped<ITreatmentService, DummyTreatmentService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// setting up scalar 
app.MapSwagger("/openapi/{documentName}.json");
app.MapScalarApiReference(options =>
{
    options.Title = "ManeManagerBackendAPI";
    options.Theme = ScalarTheme.DeepSpace;
});

app.Run();
