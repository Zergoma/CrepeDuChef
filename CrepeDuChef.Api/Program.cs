using CrepeDuChef.Api.DI;
using CrepeDuChef.Api.Endpoints;
using CrepeDuChef.Infrastructure.DbContexts;
using CrepeDuChef.Infrastructure.DI;

using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

using InfraDbContext = CrepeDuChef.Infrastructure.DbContexts;

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

// --- DbContext ---
builder.Services.AddDbContextFactory<InfraDbContext.CrepeDbContext>(options =>
{
    //options.UseSqlServer(connectionString));
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCrepeDuChefInfrastructure();
builder.Services.AddCrepeBackendServices();


var app = builder.Build();

// create db
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CrepeDbContext>>();
    using var db = factory.CreateDbContext();
    db.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapCrepePartyEndpoints();
app.MapChefsEndpoints();


app.Run();



