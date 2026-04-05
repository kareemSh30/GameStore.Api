using System;
using GameStore.Data;
using GameStore.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenreEndPoint
{
 public static void MapGenreEndpoints(this WebApplication app)
 {
     var group = app.MapGroup("/genres");

     //get all genres
     group.MapGet("/", async (GameStoreContext context) =>
     {
         return await context.Genres
             .Select(genre => new GenreDto(genre.Id, genre.Name))
             .AsNoTracking()
             .ToListAsync();
     });
}
}
