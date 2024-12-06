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
    //FusionCache => ("population") anahtarý ile önbellekte veri var mý yok mu kontrol eder.
    //Önbellekte iliþkilendirilmiþ bir veri yoksa ikinci parametre olarak saðlanan lambda fonks. çalýþtýrýlýr ve dönen deðeri önbelleðe kaydeder. Eðer önbellekte veri varsa, yeni bir sorgu yapmadan doðrudan önbellekteki veriyi döner.
    //Süre => Bu süre verinin önbellekte geçerli olduðu süreyi belirtir. O süre boyunca önbellekte saklanýr, süre dolduktan sonra, tekrar API'ye istek yapýlýr.
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
