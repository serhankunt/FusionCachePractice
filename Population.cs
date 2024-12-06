using Newtonsoft.Json;

namespace FusionCachePractice;

internal class PopulationResponse
{
    [JsonProperty("data")]
    public Population[] Data { get; set; } = default!;
}
public class Population
{
    [JsonProperty("ID Nation")]
    public string IdNation { get; set; } = default!;

    [JsonProperty("Nation")]
    public string Nation { get; set; } = default!;

    [JsonProperty("ID Year")]
    public string IdYear { get; set; } = default!;

    [JsonProperty("Year")]
    public string Year { get; set; } = default!;

    [JsonProperty("Population")]
    public string TotalPopulation { get; set; } = default!;

    [JsonProperty("Slug Nation")]
    public string SlugNation { get; set; } = default!;
}
