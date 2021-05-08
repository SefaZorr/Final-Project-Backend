using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Business.Constants;
using Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace Business.BusinessAspects.Autofac
{
    #region Not
    //Yetkilendirmeyi yapmak için bir aspect yazıyoruz.Constructer'da bana rolleri ver diyoruz rolleride virgülle ayırarak verebiliriz demiştrik constructerın içinde ise
    //_roles = roles.Split(','); bir metni bizim belirledigimiz karaktere göre ayırıp array'e atıyor. IHttpContextAccessor ise httpcontext adı üstünde siz bir istek yapıyorsun
    //uz jwt token da gönderekek bir istek yapıyoruz ya sonuçta oraya binlerce kişi istek yapabilir her bir istek için bir httpcontext'i oluşturur yani thread oluşur.
    //ServiceTool nromalde controller business çagırıyor business dal'ı çagırıyor bu zincirin içinde aspect yok aspect bambaşka o yüzden buraya injection yaparsan başarılı
    //olamazsınız asp.net web api bunu göremez o yüzden dolayısıyla bu bambaşka bir olay yer zincirin içinde degil aspect o yüzden bende o dependenyleri yakalabilmek için
    //bir tane ServiceTool yazdık ServiceTool bizim injection alt yapımızı aynen okuyabilmemizi yarayan bir araç olucak.
    //Örnegin elinde productService = ServiceTool.ServiceProvider.GetService<IProductService>(); bu gidip Autofacte yaptıgımız injection'ın degelerini alıcak.
    #endregion
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        #region Not
        //On before MethodInterception dan geliyor yani bu metodun önünde çalıştır demek.Peki nabıyor bu o anki kullanıcının ClaimRoles'larını bul bakalım diyor.Sonra bu
        //kullanıcının rollerini gez eger claimlerinin içinde ilgili rol varsa return et yani metodu çalıştırmaya devam et demek olaki yok o zaman bir tane AuthorizationDenied
        //yetkin yok hatası ver. 
        #endregion
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
