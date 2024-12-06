using FusionCachePractice;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("Population");

builder.Services.AddSingleton<IPopulationProvider, PopulationProvider>();

var options = new FusionCacheOptions()
{
    DefaultEntryOptions = new FusionCacheEntryOptions()
    {
        Duration = TimeSpan.FromMinutes(1),
        IsFailSafeEnabled = true,
        FailSafeMaxDuration = TimeSpan.FromHours(2),
        FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
    }
};

builder.Services.AddFusionCache().TryWithAutoSetup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/population", async (IPopulationProvider populationProvider, IFusionCache cache) =>
{
    var population = await cache.GetOrSetAsync("population", async _ =>
    //FusionCache => ("population") anahtar� ile �nbellekte veri var m� yok mu kontrol eder.
    //�nbellekte ili�kilendirilmi� bir veri yoksa ikinci parametre olarak sa�lanan lambda fonks. �al��t�r�l�r ve d�nen de�eri �nbelle�e kaydeder. E�er �nbellekte veri varsa, yeni bir sorgu yapmadan do�rudan �nbellekteki veriyi d�ner.
    //S�re => Bu s�re verinin �nbellekte ge�erli oldu�u s�reyi belirtir. O s�re boyunca �nbellekte saklan�r, s�re dolduktan sonra, tekrar API'ye istek yap�l�r.
    {
        return await populationProvider.GetPopulations();
    }, TimeSpan.FromSeconds(5));

    return population != null ? Results.Json(population) : null;
})
.WithName("GetPopulation")
.WithOpenApi();

app.UseAuthorization();

app.MapControllers();

app.Run();
