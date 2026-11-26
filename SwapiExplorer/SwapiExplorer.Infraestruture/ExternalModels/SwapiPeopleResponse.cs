using System.Text.Json.Serialization;

namespace SwapiExplorer.Infrastructure.ExternalModels;

internal class SwapiPeopleResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; } = string.Empty;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;

    // URL do planeta natal, ex: "https://swapi.dev/api/planets/1/". Vamos resolver o nome depois.
    [JsonPropertyName("homeworld")]
    public string Homeworld { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}