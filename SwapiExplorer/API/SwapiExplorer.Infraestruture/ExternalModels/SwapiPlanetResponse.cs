using System.Text.Json.Serialization;

namespace SwapiExplorer.Infrastructure.ExternalModels;

// Esta classe existe SÓ para desserializar o JSON que a SWAPI devolve.
// Ela fica isolada na Infrastructure — se a SWAPI mudar o formato, só isso muda.
internal class SwapiPlanetResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("climate")]
    public string Climate { get; set; } = string.Empty;

    [JsonPropertyName("terrain")]
    public string Terrain { get; set; } = string.Empty;

    [JsonPropertyName("population")]
    public string Population { get; set; } = string.Empty;

    // A SWAPI devolve a própria URL do recurso, ex: "https://swapi.dev/api/planets/1/"
    // Usamos isso para extrair o Id numérico.
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

// Envelope de paginação que a SWAPI usa em listagens: { count, next, previous, results: [...] }
internal class SwapiListResponse<T>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = new();
}