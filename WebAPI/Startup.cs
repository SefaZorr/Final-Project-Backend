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
        //Web apinin kendi i�erisinde bir IoC yap�s� vard�r.Bana arka planda bir referans olu�tur demek k�sacas� IoC'ler bizim yerimize new'liyor biz diyorizki birisi senden
        //IProductService istersen ona arka planda ProductManager olu�tur onu ver demek.AddSingleton ayn� tek bir instance'� �retmeyi sagl�yor i�erisinde data tutmuyorsak
        //singleton kullanabiliriz.K�sacas� bu �u demek biri Constructur'da IProductService isterse ona arka planda bir tane ProductManager new'i ver demek.Bu yap�y� daha
        //farkl� bir mimariye ta��yor olacag�z bu mimarinin ad� Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DyrInject bunlar .net projelerinde a�ag�daki adam�n
        //yapt�g� harekti yap�yor bunlar daha .net 'de built in de IoC yap�s� yokken daha bu adamlar bu �ekilde �al��mak isteyenler i�in alt yap� sunuyor.Autofac bize AOP
        //imkani sunuyor o y�zden .net'in kendi IoC container'na Autofac enjekte edicez. 
        //Autofact'te a�ag�daki yap�n�n ayn�s� yap�yor.Bu IOC yap�land�rmas�n� hangi interface 'in kar��l�g� nedir �eklinde yap�land�rmay� api k�sm�nda degil biraz daha
        //backend k�sm�nda yapman�n(Business k�sm�nda yap�caz) �ok fazla avantaj� var nedir bunlar mesela biz burda startup da yaparsak yar�n �b�rg�n bir tane daha api 
        //eklersek veya bamba�ka bir servis mimarisi eklersek bizim b�t�n konfigurasyonumuz api'de kal�yor asl�nda bu konfigurasyonu dogru yapmka i�in yerimiz buras� olmamal�.
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
