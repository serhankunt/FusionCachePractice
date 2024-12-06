# Fusion Cache Nedir ? 
 
	- Fusion Cache, .NET uygulamalar�nda y�ksek performansl� ve esnek bir cache y�ntemi sa�layan bir k�t�phanedir. Cacheleme i�lemlerinde s�kl�kla kar��la��lan sorunlar� ��zmek ve hem in-memory hem de harici cache sistemleriyle kolayca entegre �al��may� sa�lamak amac�yla tasarlanm��t�r. Fusion Cache, Memory ve Distributed Cache sistemlerini bir araya getirerek ikili bir yap� sunmaktad�r.

## Fusion Cache �zellikleri Nelerdir ? 
	
1. H�zl� ve G�venilir Cache Mekanizmas�
	* Uygulama i�erisinde optimize edilmi� ve olduk�a d���k gecikme s�releri sa�lar.
	* In-Memory cacheleme sayesinde h�zl� eri�im sa�lar

2. Fail-Safe �zelli�i
	* Harici bir cache mekanizmas�nda(�rne�in => Redis) hata veya a� problemi olsa bile in-memory cache �zerinden i�lem yaparak uygulaman�n �al��maya devam etmesini sa�lar.

 3. Background Refreshing
	* Cache'deki veriler eskiyorsa, arka planda yenileyerek uygulaman�n bu s�re�te etkilenmesini engeller.
 4.Time-To-Live(TTL) ve Time-To-Fail(TTF)
	* Cache'deki veriler i�in TTL(ne kadar s�re ge�erli olaca��) ve TTF(harici cache ba�ar�s�z oldu�unda in-memory cache'in ne kadar kullan�laca��) tan�mlanabilir.
 5. Distributed Cache ile Entegrasyon
	* Redis, Memcached veya herhangi bir .NET Distributed Cache sa�lay�c�s�yla entegre edilebilir.
	


 ## Fusion Cache Nas�l Kullan�l�r ? 

	-K�t�phaneyi y�kleyin

	dotnet add package ZiggyCreatures.FusionCache

	- Temel kullan�m

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


