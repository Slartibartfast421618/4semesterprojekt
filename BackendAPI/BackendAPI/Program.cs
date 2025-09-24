using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // to use Scalar
builder.Services.AddSwaggerGen(); // to use scalar 

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
