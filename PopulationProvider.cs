using Newtonsoft.Json;

namespace FusionCachePractice;

internal interface IPopulationProvider
{
    Task<Population[]?> GetPopulations();
}

public class PopulationProvider(IHttpClientFactory httpClientFactory) : IPopulationProvider
{
    public async Task<Population[]?> GetPopulations()
    {
        using var client = httpClientFactory.CreateClient("Population");
        client.BaseAddress = new Uri("https://datausa.io/api/data?drilldowns=Nation&measures=Population");
        var response = await client.GetAsync(client.BaseAddress);
        var content = await response.Content.ReadAsStringAsync();
        var populationResponse = JsonConvert.DeserializeObject<PopulationResponse>(content);

        return populationResponse?.Data;
    }
}
