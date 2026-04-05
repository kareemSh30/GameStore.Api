using GameStore.Data;
using GameStore.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();   

var app = builder.Build();

app.MapGameEndpoints();
app.MapGenreEndpoints();

app.MigrateDb();

app.SeedData();

app.Run();
