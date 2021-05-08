using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsft
{
    #region Not
    //_memoryCache.Set() ile cache deger ekliyoruz. Duration cachede kalıcak süreyi gösterir.
    //_memoryCache.TryGetValue(key,out _); out _ bir şey döndürmesini istemiyorson böyle kullanabilirsin yani sanki ben sadece bellekte böyle bir data varmı yokmu oana bakmak
    //istiyorum cachedeki datayı verme bana demek.
    //Constructure yazmamızın sebebi _memoryCache bizim bunu injectiondan almamız gerekiyor  serviceCollection.AddMemoryCache();(bu kod CoreModule'da) bu kodla injection 
    //otomatik eklenmiş oluyor aslında burada bir memoryCache instance'ı oluşuyor arka planda dolayısıyla bende diyorumki o instance'ı bana ver.Yani kısacası siz bir
    //Desktop uygulamadan da istediniz butun enjekte edilmiş interfaceleri bununla çekebilirsiniz örnegin windows form wpf uygulamalarında built in dependency injection yok
    //apide ki gibi constructure yazıyoruz ya o zaman ne yapıcam ServiceTool ile yapıcam.
    #endregion
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        #region Not
        //Bu method ona verdigimiz bir pattern'a göre silme işlemini yapıcak.Bellekten silmeye yarıyor çalışma anında bellekten silmeye yarıyor yani siz elinizde bir sınıfın
        //instance'ı var bellekte ve ona çalışma anında müdahale etmek istiyorsunuz bunu ne ile yaparsınız reflection ile.Reflection ile çalışma anında elinizde bulunan
        //nesnelere ve hatta olmayanlarıda yeniden oluşturmak gibi çalışmalar yapabileceginiz bir yapı kısacası kodu çalışma anında oluşturma ona çalışma anında müdahele
        //etme gibi şeyleri reflection ile yapıyoruz.
        //Öncelikle biz diyoruzki bellekte cache ile ilgili olan yapıyı çekmek istiyorum ilk basamakta diyorumki git bellege bak belletek memoryCache türünde olan EntriesCollection
        //bunu nerden biliyoruz microsoft diyorki ben bunu cachledigimde bellekte cache datalarını EntriesCollection diye bir şeyin içine atıyorum diyor.Kısacası ilk basamakta
        //burda ne yapıyorum bellege git bellekteki EntriesCollection u bul diyorum.2.basamakta definition'ı MemoryCache olanı bul diyorum.4.Basamakta onların herbirini
        //her bir cache elemanını gez ve her bir cache elemaından 6.basamaktaki kural uyandan.Bu noktada 6.basamakta o cache datası içirisinde anahtarlardan benim gönderdigim
        //degere uygun olanlar varsa içerisinde onlar geçiriyorsa onları keysToRemove içine atıcak ve onları foreach ile tek tek gezzitorum onların keylerini buluyorum sonra
        //onları bellekten uçuyorum.
        #endregion
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
