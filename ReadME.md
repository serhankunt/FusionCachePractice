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


# Diyagramın Bileşenleri ve Çalışma Mantığı:

- **Method Call** (Metot Çağrısı): Sistemle etkileşimin başladığı noktadır. GetOrSet, Set, Remove gibi metodlar aracılığıyla veriler okunur, yazılır veya silinir.
- **Memory Cache (Bellek Önbellek)**: En hızlı erişilebilen veri saklama alanı. Sık kullanılan veriler burada tutulur, böylece disk veya ağa erişim ihtiyacı azalır.
- **Distributed Cache (Dağıtık Önbellek)**: Birden fazla düğümde dağıtılarak daha fazla veri saklama kapasitesi sunar. Yüksek kullanımlı sistemlerde, yükün farklı düğümlere dağıtılmasını sağlar.
- **Backplane (Veri Otobüsü)**: Farklı bileşenler arasındaki veri iletişimini sağlayan bir tür kanal veya ağdır.
- **Factory**: Yeni nesnelerin oluşturulmasından sorumlu olan bir bileşendir. Örneğin, yeni bir veri öğesi eklendiğinde, Factory bu öğeyi uygun bir şekilde oluşturur ve cache'e yerleştirir.
- **Notifications (Bildirimler)**: Veri değişiklikleri olduğunda diğer düğümlere gönderilen mesajlardır. Bu sayede tüm düğümlerdeki veriler senkronize kalır.
- **Data Source (Veri Kaynağı)**: Verilerin ilk olarak alındığı yerdir (örneğin, bir veritabanı veya bir web servisi).
# Çalışma Prensibi:

- **Veri İsteği**: Bir uygulama, Fusion Cache sistemine bir veri isteği gönderir (örneğin, bir kullanıcının profil bilgilerini almak).
- **Bellek Önbelleği Kontrolü**: Sistem öncelikle istediği veriyi en hızlı erişilebilen bellek önbelleğinde arar. Eğer veri burada bulunursa, doğrudan uygulamaya gönderilir.
- **Dağıtık Önbellek Kontrolü**: Veri bellek önbelleğinde yoksa, dağıtık önbellekte aranır. Bu daha yavaş olabilir, ancak daha fazla veri saklama kapasitesi sunar.
- **Veri Kaynağı**: Veri hem bellek hem de dağıtık önbelleklerde bulunamadığında, veri kaynağından alınır.

- **Önbelleğe Kaydetme**: Veri kaynağından alınan veri, hem bellek hem de dağıtık önbelleğe kaydedilir. Böylece sonraki isteklerde daha hızlı erişim sağlanır.
- **Bildirim**: Veri değişikliği olduğunda, diğer düğümlere bildirim gönderilir ve dağıtık önbellek güncellenir.

