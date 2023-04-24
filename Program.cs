using Microsoft.OpenApi.Models;
using robotstakeover.DB;

var builder = WebApplication.CreateBuilder(args);
// Adding CORS functionality
builder.Services.AddCors(options => {});

// Adding converter for converting date as string to DateOnly and vice versa
builder.Services.AddDateOnlyTimeOnlyStringConverters();

// Adding swagger to the API for documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "ROBOT Apocalypse API", Description = "Keeping track of all survivors, their resources, and the robots", Version = "v1" });
});

var app = builder.Build();

// using swagger in the API
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World! -from the apocalypse");
app.MapGet("/survivors", () => RobotstakeoverDB.GetSurvivors());

app.Run();
