using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    #region Not
    //CacheRemove aspect ne zaman çalışır datanız bozuldugu zaman ne zama bozulur yeni data eklenirse güncellenirse silinirse bozulur o yüzden cache yönetimi yapıyorsanız
    //o managerda veriyi manipule eden metotlarına cacheremove aspect uygularız.Onsuccess 'te uyguluyor niye onsuccess belkide add işlemi hata vericek veritabanına ürün
    //ekleyemicek hata vericek ben niye cache sileyim o yüzden onsuccess diyoruz metot başarılı oldugunda çalışsın demek. 
    #endregion
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
