using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    #region Not
    //İsmine AutofacBusinesModule dememizin sebebi niye businessmodule diyoruz çünkü bu projeyi ilgilendirin konfigürasyonu burada yapıcaz bunun birde core'da yapılan
    //versiyonu var core versiyonu evrensel olucak A projesinde, B projesinde ,C projesindede olucak configurasyonları oraya koyacagız ayrı ayrı set etmeyelim diye.
    //Startup'ta yaptıgımız oratamı autofac'te Module'dan kalıtım alarak böyle kuruyoruz.builder.RegisterType startup'ta services.AddSingleton<>'na karşılık geliyor.
    //builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); birisi senden IProductService isterse ProductManager instance'ı ver ona.
    #endregion
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


            #region Not
            //Burası şimdi yukarıda bizi kayıt ettigimiz sınıflar peki biz ne dedik neden Autofac kullanıyoruz dedik çünkü Autofac kullanmamızın sebebi .net mimarisindede var
            //evet ama Autofac bize aynı zamanda Interceptor görevi veriyor bak ne yapıyor  diyorki çalışan uygulama içerisinde implemente edilmiş inteface leri bul diyor
            //onlara onlar için AspectInterceptorSelector'ı çagır diyor.Kısacası Auotofac bizim bütün sınıflarımız için önce AspectInterceptorSelector çalıştırıyor git bak 
            //diyor bu adamın bir aspect'i varmı [] ile yazdıgımız varmı diye bak diyor. 
            #endregion
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
