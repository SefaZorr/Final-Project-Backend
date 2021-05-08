using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    #region Not
    //AutofacBusinessModule bizim proje seviyesindeki injectionlarımızdır northwinde kullanacagımız şeyler ama tüm projelerimizdeki yapıları injectionları bidaha bidaha
    //injection yapmamak için core'a taşıyor olacagız.Bu bizim tüm projelerimizde hatta ve hatta şirket degiştirdigimizde bile kullanabilecegimiz yapıdır.
    //IServiceCollection bu o servisleri bu arkadaşımız yüklüyor olsun.
    #endregion
    public interface ICoreModule
    { 
        void Load(IServiceCollection serviceCollection);
    }
}
