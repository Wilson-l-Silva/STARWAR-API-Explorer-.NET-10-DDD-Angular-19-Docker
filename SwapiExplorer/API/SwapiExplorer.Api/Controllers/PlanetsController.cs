using Microsoft.AspNetCore.Mvc;
using SwapiExplorer.Application.DTOs;
using SwapiExplorer.Application.Interfaces;

namespace SwapiExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // resultado: /api/planets
public class PlanetsController : ControllerBase
{
    // O Controller depende SOMENTE da interface da Application — nunca conhece a Infrastructure.
    private readonly IStarWarsAppService _starWarsAppService;

    public PlanetsController(IStarWarsAppService starWarsAppService)
    {
        _starWarsAppService = starWarsAppService;
    }

    // GET /api/planets?page=1
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PlanetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PlanetDto>>> GetAll(
        [FromQuery] int page = 1,
        CancellationToken cancellationToken = default)
    {
        var planets = await _starWarsAppService.ListPlanetsAsync(page, cancellationToken);
        return Ok(planets);
    }

    // GET /api/planets/1
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PlanetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanetDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var planet = await _starWarsAppService.GetPlanetAsync(id, cancellationToken);

        // Padrão REST: devolve 404 quando o recurso não existe, em vez de 200 com corpo vazio.
        if (planet is null) return NotFound();

        return Ok(planet);
    }
}