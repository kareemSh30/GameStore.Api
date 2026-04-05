using System;
using GameStore.Data;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public  static class GameEndpoints
{
    const string GetGameByIdEndpointName = "GetGameById";
    private static  readonly List<GameDto> games =
[
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateTime(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateTime(2017, 10, 27)),
    new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99m, new DateTime(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 39.99m, new DateTime(2015, 5, 19)),
    new GameDto(5, "Minecraft", "Sandbox", 26.95m, new DateTime(2011, 11, 18))
];


public static void MapGameEndpoints(this WebApplication app)
{

    var group = app.MapGroup("/games");
    
//get all games

group.MapGet("/", async   (GameStoreContext context) =>
 await context.Games
 .Include(game => game.Genre)
 .Select(game => new GameDto(
    game.Id,
    game.Name,
    game.Genre!.Name, 
    game.Price,
    game.ReleaseDate))
    .AsNoTracking()
    .ToListAsync());



group.MapGet("/{id}",async (int id,GameStoreContext context) =>
{

   var game= await context.Games.FindAsync(id);
   
   return game is null ? Results.NotFound() : Results.Ok(
    new GameDetailsDto(
    game.Id,
    game.Name,
    game.GenreId, 
    game.Price,
    game.ReleaseDate));
}).WithName(GetGameByIdEndpointName);

//post / games
group.MapPost("/", async (CreateGamesDto newGame, GameStoreContext context) =>
{
   
   Game game= new()
   {
         Name = newGame.Name,
         GenreId = newGame.GenreId,
         Price = newGame.Price,
         ReleaseDate = newGame.ReleaseDate
   };
    context.Games.Add(game);
    await context.SaveChangesAsync();

    GameDetailsDto gameDetails = new(game.Id,
     game.Name, 
     game.GenreId, 
     game.Price,
      game.ReleaseDate);

    return Results.CreatedAtRoute(GetGameByIdEndpointName, new { id = gameDetails.Id }, gameDetails);
});

//put / games / {id}
group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext context) =>
{
    var index =await context.Games.FindAsync(id);   
    if (index is null)
        return Results.NotFound();

    index.Name = updatedGame.Name;
    index.GenreId = updatedGame.GenreId;
    index.Price = updatedGame.Price;
    index.ReleaseDate = updatedGame.ReleaseDate;
    await context.SaveChangesAsync();

    return Results.NoContent();
});

//delete / games / {id}
group.MapDelete("/{id}", async (int id, GameStoreContext context) =>
{
    var game = await context.Games.FindAsync(id);
    if (game is null)
        return Results.NotFound();

    context.Games.Remove(game);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

}
}
