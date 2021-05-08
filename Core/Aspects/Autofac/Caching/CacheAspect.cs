using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    #region Not
    //Intercept override et diyoruz bu MethodInterception dan geliyor bu life cycle ı harekete geçir demek OnBefore'da yapabilirdik burada fark etmez.Invocation metodumuz
    //kısacası GetAll'ı çalıştırmadan bu kodları çalıştırıyoruz.
    //CacheAspect kendisi bir constructure var kendisi bir attribute zaten baakın default deger vermişiz 60 demekki biz süre vermezsek bu veri 60 dk boyunca cache'te kalıcak
    //sonra cache'ten otomatikmen sistem onu cache'ten bellekten atıcak.Sonra demişizki ICacheManager olarak buda bir aspect oldugu için injection burada yapamayız construc
    //ture'da dolayısıyla yine serviceTool'u kullanarak hangi cacheManager'ı kullandıgımızı belirtiyoruz.invocation.Method.ReflectedType.FullName bu kod bize namespace + class
    //'ın ismini verir.invocation.Method.Name bunla beraber ise metodun ismini veriyor bize.Yani ilk kısımı bize Northwind.Business.IProductService.GetAll diye veriyor.
    //arguments ise bize metodun parametlerini listeye çevir diyoruz varsa tabiki.Sonra metodun parametrelerini tek tek eger parametre degeri varsa o parametre degerini burda
    //getAll içine ekliyoruz eger parametre vermediysek Null yazıyor.Sonra git bak bakalım bellekte böyle bir cache anahtarı varmı eger cachte varsa invocation.ReturnValue bu
    //şu demek sen metodu hiç çalıştırmadan şimdi geri dön kendin manuel bir return oluşturuyorsun  _cacheManager.Get(key) ile eger yoksa invocation.Proceed(); metodu çalıştır
    //devam et diyoruz.Metod çalıştı şimdi metot çalışınca veri tabanına gidiyor datayı getiriyor sonrasında _cacheManager.Add(key, invocation.ReturnValue, _duration); ile
    //cache ekliyoruz. 
    #endregion
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }

}
