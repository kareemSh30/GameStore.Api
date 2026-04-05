using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record  GameDto(
    [Required][StringLength(50)] int Id,
    [Required][StringLength(50)] string Name,
    [Required][StringLength(15)] string Genre,
    [Required] [Range(0, 100)] decimal Price,
    [Required] DateTime ReleaseDate
);