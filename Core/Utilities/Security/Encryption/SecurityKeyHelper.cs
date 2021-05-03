using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    #region Not
    //Peki bu SecurityKeyHelper nedir işin içerisinde şifreleme olan sistemlerde bizim herşeyi bir byte[] array formatında veriyor olmamız gerekiyor yani basit bir string
    //ile bir key oluşturamıyoruz.Bunları asp.net jwt servislerinin anlayacagı hale getirmemiz gerekiyor.
    //SecurityKey döndüren bir CerateSecurityKey metodu yazıyoruz parametre olarak bana bir tane string securityKey ver diyorum bende ona SecurityKey karşılıgını vereyim
    //diyorum.Yani burası bizim elimizde bir tane uyduruk bir string var appsettings.json'da biz bu uyduruk stringlerle encryption'a parametre geçemiyoruz onu bir byte array
    //haline getiriyor burası.
    #endregion
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
