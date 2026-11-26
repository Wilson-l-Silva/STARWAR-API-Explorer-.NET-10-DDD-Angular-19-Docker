using Microsoft.AspNetCore.Mvc;
using SwapiExplorer.Application.DTOs;
using SwapiExplorer.Application.Interfaces;

namespace SwapiExplorer.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // resultado: /api/characters
public class CharactersController : ControllerBase
{
    private readonly IStarWarsAppService _starWarsAppService;

    public CharactersController(IStarWarsAppService starWarsAppService)
    {
        _starWarsAppService = starWarsAppService;
    }

    // GET /api/characters?page=1
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetAll(
        [FromQuery] int page = 1,
        CancellationToken cancellationToken = default)
    {
        var characters = await _starWarsAppService.ListCharactersAsync(page, cancellationToken);
        return Ok(characters);
    }

    // GET /api/characters/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var character = await _starWarsAppService.GetCharacterAsync(id, cancellationToken);
        if (character is null) return NotFound();

        return Ok(character);
    }
}