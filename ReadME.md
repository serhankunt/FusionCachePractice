# Fusion Cache Nedir ? 
 
	- Fusion Cache, .NET uygulamalarında yüksek performanslı ve esnek bir cache yöntemi sağlayan bir kütüphanedir. Cacheleme işlemlerinde sıklıkla karşılaşılan sorunları çözmek ve hem in-memory hem de harici cache sistemleriyle kolayca entegre çalışmayı sağlamak amacıyla tasarlanmıştır. Fusion Cache, Memory ve Distributed Cache sistemlerini bir araya getirerek ikili bir yapı sunmaktadır.

## Fusion Cache Özellikleri Nelerdir ? 
	
1. Hızlı ve Güvenilir Cache Mekanizması
	* Uygulama içerisinde optimize edilmiş ve oldukça düşük gecikme süreleri sağlar.
	* In-Memory cacheleme sayesinde hızlı erişim sağlar

2. Fail-Safe Özelliği
	* Harici bir cache mekanizmasında(örneğin => Redis) hata veya ağ problemi olsa bile in-memory cache üzerinden işlem yaparak uygulamanın çalışmaya devam etmesini sağlar.

 3. Background Refreshing
	* Cache'deki veriler eskiyorsa, arka planda yenileyerek uygulamanın bu süreçte etkilenmesini engeller.
 4.Time-To-Live(TTL) ve Time-To-Fail(TTF)
	* Cache'deki veriler için TTL(ne kadar süre geçerli olacağı) ve TTF(harici cache başarısız olduğunda in-memory cache'in ne kadar kullanılacağı) tanımlanabilir.
 5. Distributed Cache ile Entegrasyon
	* Redis, Memcached veya herhangi bir .NET Distributed Cache sağlayıcısıyla entegre edilebilir.
	


 ## Fusion Cache Nasıl Kullanılır ? 

	-Kütüphaneyi yükleyin

	dotnet add package ZiggyCreatures.FusionCache

	- Temel kullanım

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


![FusionCacheDiagram](https://github.com/user-attachments/assets/49d5855b-1036-4476-b0fe-83e0b63a21b4)

