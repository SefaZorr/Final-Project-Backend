using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolver;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors();


            #region Not
            //Bu k�s�mda bir bu sistemde Authentication olarak JwtBearerDefaults token kullan�lacak haberin olsun dedigimiz yer buras� yani biz Asp.net web apiye diyorizki
            //bu sistemde jwt kullan�lacak haberin olsun.
            //HttpContextAccessor asl�nda her yap�lan istekle ile ilgili olu�an context diyebiliriz yani bizim clientimiz bir istek yapt�g�nda o istegin ba�lang�c�ndan
            //biti�ine kadar yani istek request in yap�lmas�ndan yan�t response verilmesine kadar ki s�re�te o kullan�c�n�n o isteginin takip edilmesi i�lemini bu 
            //HttpContextAccessor yap�yor.
            #endregion

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            #region Not
            //Middleware Asp.net ya�am d�ng�s�nde hangi yap�lar�n s�ras�yla devreye girecegini s�yl�yoruz.�nceden klasik Asp.net uygulamlar�nda bunlar b�yle tan�ml�yd� ama
            //ihtiyac�n�z olsun olmas�n devreye giriyordu art�k �yle degil sizin neye ihtiyac�n�z varsa onu o araya sokuyorsunuz. 
            #endregion
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
