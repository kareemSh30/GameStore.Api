using System.ComponentModel.DataAnnotations;

namespace GameStore;

public record  CreateGamesDto(
    [Required][StringLength(50)] string Name,
    [Range(1,50)] int GenreId,
    [Required] [Range(0, 100)] decimal Price,
    [Required] DateTime ReleaseDate
);
