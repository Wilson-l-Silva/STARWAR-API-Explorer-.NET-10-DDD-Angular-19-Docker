
namespace SwapiExplorer.Application.DTOs;

// DTO (Data Transfer Object): formato "achatado", pensado para ser serializado em JSON
// e devolvido pela API. Nunca expomos a entidade de Domínio diretamente na API
// (isso evita acoplar o contrato público da API às regras internas de negócio).
public record PlanetDto(
    int Id,
    string Name,
    string Climate,
    string Terrain,
    string Population,
    bool IsPopulated);
