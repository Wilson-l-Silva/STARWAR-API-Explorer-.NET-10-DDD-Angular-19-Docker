namespace SwapiExplorer.Infrastructure.Helpers;

// A SWAPI não devolve um "id" solto; ela devolve a URL do recurso.
// Ex: "https://swapi.dev/api/planets/1/" -> queremos extrair o número 1.
internal static class SwapiUrlHelper
{
    public static int ExtractId(string url)
    {
        // Remove a barra final, depois pega o último segmento do caminho.
        var trimmed = url.TrimEnd('/');
        var lastSegment = trimmed.Split('/').Last();

        return int.TryParse(lastSegment, out var id) ? id : 0;
    }
}