using System;
using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public static class DataExtensions
{
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStore");
        
        builder.Services.AddSqlite<GameStoreContext>(connString);
    }

    public static void MigrateDb(this WebApplication app)
    {
        using var scope=app.Services.CreateScope();
        var dbContext=scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Sports" }
            );
            context.SaveChanges();
        }
    }
}
