using CrepeDuChef.Api.Contracts.CrepeParties;
using CrepeDuChef.Infrastructure;
using CrepeDuChef.Infrastructure.DI;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

using AppInterface = CrepeDuChef.Application.Interfaces;
using AppServices = CrepeDuChef.Application.Services;
using InfraDbContext = CrepeDuChef.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddLocalization();

string dbPath =
    Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "CrepeDuChef.db3"
        );
builder.Services.AddDbContext<CrepeDbContext>(options =>
    //options.UseSqlServer(connectionString));
    options.UseSqlite("Data Source=server-temp.db"));

builder.Services.AddCrepeDuChefInfrastructure();



// --- DbContext ---
builder.Services.AddDbContext<InfraDbContext.CrepeDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

// --- Application services ---
builder.Services.AddScoped<AppInterface.ICrepePartyService, AppServices.CrepePartyService>();

// --- Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrepeDuChef API", Version = "v1" });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // UI Swagger qui lit /openapi/v1.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "CrepeDuChef API v1");
    });
}

app.UseHttpsRedirection();


app.MapGet("/ping", () => "pong");

app.MapPost("/sessions", async (
    AddCrepePartyRequest req,
    AppInterface.ICrepePartyService service
) =>
{
    await service.AddCrepePartyAsync(req.Data, req.DeviceId);
    return Results.Ok();
});



app.Run();



