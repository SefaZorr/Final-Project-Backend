using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    #region Not
    //IServiceCollection .net servis collectionını yani servislerini kullanarak servislerini al ve onları built et kendin.Kısacası bu kod sizin o web Api de veya autofacte
    //oluşturdugumuz injectionlar varya o injectionları oluşturabilmemizi yarıyor. 
    #endregion
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
