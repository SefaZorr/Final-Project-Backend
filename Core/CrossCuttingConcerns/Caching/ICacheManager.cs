using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    #region Not
    //Bu interface teknoloji bagımsız cache yazmamızı saglayacak.Cache olayında öncelikle mesela cache e bir şey ekleyebiliriz key,value tipinde olucak gelecek data object
    //tipinde olucak herşeyin base'i object dir ve bir de bu cache'te ne kadar duracak duration şeklinde dakika yada saat cinsinde parametre ekliyoruz.
    //Cache'ten veri getirmek için ise generic metod  Get yazıyoruz.object Get(string key) farklı bir kullanım şekli.
    //IsAdd() şimdi bizim cache'e eklerken örnegin GetAllByCategoryId() bunu cache'ten mi getirelim yoksa veritabanındamı getirelim buna nasıl karar veririz eger cache'te
    //varsa cache'ten getiriz yoksa veritabanından getiriz ama onu cache ekleriz.
    //void Remove(string key) sana bir key versem sen onu cache'ten uçururmusun.
    //void RemoveByPattern(string pattern); bunda ise yukarıda oldugu gibi hangi key'i vericez bir sürü olabilir dolayısıyla bir pattern yazsak mesela ismi get ile başlayanlar
    //ı oluştur yada key de ismi category olanları uçur gibi.
    #endregion
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value,int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
