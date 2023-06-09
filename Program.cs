using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using robotstakeover;
using robotstakeover.DB;

var builder = WebApplication.CreateBuilder(args);
// Adding CORS functionality
builder.Services.AddCors(options => { });

// custom DateOnly Json Converter
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    // options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Adding swagger to the API for documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ROBOT Apocalypse API", Description = "Keeping track of all survivors, their resources, and the robots", Version = "v1" });
    c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string" });
    c.EnableAnnotations();
});

var app = builder.Build();

// using swagger in the API
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// Check infections before starting app
RobotstakeoverDB.checkInfections();
// Connect to Robots CPU system and retrive list of robots
RobotstakeoverDB.GetRobotsFromCPU();

app.MapGet("/", () => "Hello World! -from the apocalypse");
// Survivors endpoints
app.MapGet("/getsurvivors", () => RobotstakeoverDB.GetSurvivors());
app.MapGet("/getsurvivor/{id}", (int id) => RobotstakeoverDB.GetSurvivor(id));
app.MapPost("/addsurvivor", (Survivor survivor) => RobotstakeoverDB.CreateSurvivor(survivor));
app.MapPost("/updatesurvivor", (Survivor survivor) => RobotstakeoverDB.UpdateSurvivor(survivor));
app.MapDelete("/removesurvivor/{id}", (int id) => RobotstakeoverDB.RemoveSurvivor(id));
// Resources endpoints
app.MapGet("/getresources", () => RobotstakeoverDB.GetResources());
app.MapGet("/getresourcesbysurvivor/{survivorid}", (int survivorid) => RobotstakeoverDB.GetResourcesBySurvivor(survivorid));
app.MapGet("/getresource/{id}", (int id) => RobotstakeoverDB.GetResource(id));
app.MapPost("/addresource", (Resource resource) => RobotstakeoverDB.CreateResource(resource));
app.MapPost("/updateresource", (Resource resource) => RobotstakeoverDB.UpdateResource(resource));
app.MapDelete("/removeresource/{id}", (int id) => RobotstakeoverDB.RemoveResource(id));
// Robots endpoints
app.MapGet("/getrobots", () => RobotstakeoverDB.GetRobots());
app.MapGet("/getlandrobots", () => RobotstakeoverDB.GetLandRobots());
app.MapGet("/getflyingrobots", () => RobotstakeoverDB.GetFlyingRobots());
app.MapGet("/getrobot/{serialNumber}", (string serialNumber) => RobotstakeoverDB.GetRobot(serialNumber));
// Infections endpoints
app.MapGet("/getinfectionspercentages", () => RobotstakeoverDB.GetInfectionsPercentages());
app.MapGet("/getinfectedsurvivors", () => RobotstakeoverDB.GetInfectedSurvivors());
app.MapGet("/getuninfectedsurvivors", () => RobotstakeoverDB.GetUninfectedSurvivors());

app.Run();
