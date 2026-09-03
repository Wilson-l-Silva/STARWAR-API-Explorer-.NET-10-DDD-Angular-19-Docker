

namespace SwapiExplorer.Application.DTOs;

public record CharacterDto(
    int Id,
    string Name,
    string BirthYear,
    string Gender,
    string? HomeworldName);
