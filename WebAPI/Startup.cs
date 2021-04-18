using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        #region Not
        //Web apinin kendi içerisinde bir IoC yapýsý vardýr.Bana arka planda bir referans oluþtur demek kýsacasý IoC'ler bizim yerimize new'liyor biz diyorizki birisi senden
        //IProductService istersen ona arka planda ProductManager oluþtur onu ver demek.AddSingleton ayný tek bir instance'ý üretmeyi saglýyor içerisinde data tutmuyorsak
        //singleton kullanabiliriz.Kýsacasý bu þu demek biri Constructur'da IProductService isterse ona arka planda bir tane ProductManager new'i ver demek.Bu yapýyý daha
        //farklý bir mimariye taþýyor olacagýz bu mimarinin adý Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DyrInject bunlar .net projelerinde aþagýdaki adamýn
        //yaptýgý harekti yapýyor bunlar daha .net 'de built in de IoC yapýsý yokken daha bu adamlar bu þekilde çalýþmak isteyenler için alt yapý sunuyor.Autofac bize AOP
        //imkani sunuyor o yüzden .net'in kendi IoC container'na Autofac enjekte edicez. 
        //Autofact'te aþagýdaki yapýnýn aynýsý yapýyor.Bu IOC yapýlandýrmasýný hangi interface 'in karþýlýgý nedir þeklinde yapýlandýrmayý api kýsmýnda degil biraz daha
        //backend kýsmýnda yapmanýn(Business kýsmýnda yapýcaz) çok fazla avantajý var nedir bunlar mesela biz burda startup da yaparsak yarýn öbürgün bir tane daha api 
        //eklersek veya bambaþka bir servis mimarisi eklersek bizim bütün konfigurasyonumuz api'de kalýyor aslýnda bu konfigurasyonu dogru yapmka için yerimiz burasý olmamalý.
        #endregion
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EfProductDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
