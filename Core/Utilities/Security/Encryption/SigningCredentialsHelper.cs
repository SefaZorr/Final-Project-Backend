using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        #region Not
        //Bu arkadaşımızda bizim için jwt servislerinin web apinin kullanabilecegi jwt lerin oluşturulabilmesi için credential mesela şey credential dir kullanıcı adı ve
        //parolanız sizin bir sisteme girmek için elinizde olanlardır burda ne var elimizde o sistemi kullanabilmek için birtane anahtara ihtiyacımız vardı crediential
        //dedigimiz bizim anahtarımız.Biz burada ASP.net diyorizki anahtar olarak sen bir tane hash işlemi yapıcaksın anahtar olarak bu securtiy key kullan şifreleme olarakta
        //güvenlik algoritmalarından hcmsha512 kullan diyorsun burada.
        //Biz HashingHelper sınıfında kendimiz birşeyleri hashelerken ve dogrularken kendimiz orada söyledik zaten HmacSha512 bunu kullanmasını gerektigini ama aynı şekilde
        //bu sisteme aynı şekilde Asp.net in kendisinde ihtiyacı var web apinin kendisinide ihtiyacı var nasıl gelen bir jwt dogrulaması gerekiyor biz burada bu arkadaşımza
        //diyoriz ki hangi anahtarı ve hangi algoritmayı kullanlıacak onu söylüyoruz.
        //Burası ise adama diyorizki burada sen bir JWT sistemini yöneticeksin senin güvenlik anahtarın budur şifreleme algoritmanda budur.
        #endregion
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
