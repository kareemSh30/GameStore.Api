using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record  UpdateGameDto
(
   [Required][StringLength(50)] string Name,
  [Range(1, 50)] int GenreId,
    [Range(0, 100)] decimal Price,
    [Required] DateTime ReleaseDate
);
