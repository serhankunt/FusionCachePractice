# Fusion Cache Nedir ? 
 
	- Fusion Cache, .NET uygulamalarýnda yüksek performanslý ve esnek bir cache yöntemi saðlayan bir kütüphanedir. Cacheleme iþlemlerinde sýklýkla karþýlaþýlan sorunlarý çözmek ve hem in-memory hem de harici cache sistemleriyle kolayca entegre çalýþmayý saðlamak amacýyla tasarlanmýþtýr. Fusion Cache, Memory ve Distributed Cache sistemlerini bir araya getirerek ikili bir yapý sunmaktadýr.

## Fusion Cache Özellikleri Nelerdir ? 
	
1. Hýzlý ve Güvenilir Cache Mekanizmasý
	* Uygulama içerisinde optimize edilmiþ ve oldukça düþük gecikme süreleri saðlar.
	* In-Memory cacheleme sayesinde hýzlý eriþim saðlar

2. Fail-Safe Özelliði
	* Harici bir cache mekanizmasýnda(örneðin => Redis) hata veya að problemi olsa bile in-memory cache üzerinden iþlem yaparak uygulamanýn çalýþmaya devam etmesini saðlar.

 3. Background Refreshing
	* Cache'deki veriler eskiyorsa, arka planda yenileyerek uygulamanýn bu süreçte etkilenmesini engeller.
 4.Time-To-Live(TTL) ve Time-To-Fail(TTF)
	* Cache'deki veriler için TTL(ne kadar süre geçerli olacaðý) ve TTF(harici cache baþarýsýz olduðunda in-memory cache'in ne kadar kullanýlacaðý) tanýmlanabilir.
 5. Distributed Cache ile Entegrasyon
	* Redis, Memcached veya herhangi bir .NET Distributed Cache saðlayýcýsýyla entegre edilebilir.
	


 ## Fusion Cache Nasýl Kullanýlýr ? 

	-Kütüphaneyi yükleyin

	dotnet add package ZiggyCreatures.FusionCache

	- Temel kullaným

	using ZiggyCreatures.FusionCache;
	var cache = new FusionCache(new FucisionCacheOptions());
	cache.Set("key","value", TimeSpan.FromMinutes(5));
	var value = cache.Get<string>("key");
	var data = cache.GetOrSet("key", ()=> "computed value", TimeSpan.FromMinutes(5));

	-Distributed Cache ile Entegrasyon 

	using Microsoft.Extensions.Caching.Distributed;
	using ZiggyCreatures.FusionCache;

	var memoryCache = new MemoryDistributedCache(new DistributedCacheOptions());
	var cache = new FusionCache(new FusionCacheOption())
				.SetupDistributedCache(memoryCache);

	cache.Set("key","distributed value", TimeSpan.FromMinutes(10));
	var distributedValue =  cache.Get<string>("key");


