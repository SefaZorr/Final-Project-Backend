using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        #region Not
        //IConfiguration sizin apinizdeki appsettings.json daki degeleri okumamıza yarıyor. 
        //_accessTokenExpiration access token'ın ne zaman pasifleşecek onu söylüyor bize.
        //_tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>() bu ise appsettings.json daki TokenOptions bölümü al ve onu TokenOptions sınıfınının
        //degerlerini alarak mapple yani Audience sınıftaki Audience gibi felan.
        #endregion
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

        }
        #region Not
        //Jwt helper nabıcak basit bir şekilde CreateToken metodunu implemente ediyor olucak.Bir tokenda neler olması gerekiyor _accessTokenExpiration bu token ne zaman
        //biticek.Configurasyon bizim asp net apı uygulamamızaki appsettings.json dosyamızı okumamıza yarıyor.SecurityKey degerinide git tokenoptions'daki SecurityKey
        //kullanarak securitykey veriyoruz yanlız securitykey i CreateSecurityKey kullanarak veriyoruz.SigningCredentials hangi anahtarı ve hangi algoritmayı kullanacagımızı
        //yazıyoruz.CreateJwtSecurityToken bunu kullanarak tokenoptions kullanarak ilgili kullanıcı için ilgili creadiantilalleri kullanarak bu adama atanacak claimleri yani
        //yetkileri içeren bir tane metotdur.
        #endregion
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        #region Not
        //Verdigimiz tokenoptions'lar kullanıcı bilgileri signingCredentials bilgisi operationClaims bilgisini vererek jwt oluşturuyoruz. 
        #endregion
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        #region Not
        //Kullanıcının kullanıcı bilgisi ve claimlerini paremetre alarak bana bir tane claim listesi oluştur diyoruz.Bu claimler oluştururken dikkat edin claim yetki dedik
        //ama yetkiden fazlası söylemiştik bakın bir jwt içerisinde sadece yetkiler olmuyor başka bilgilerde olabilir bunu aslında claim dedigimiz bu jwt kullancıyı karşılık
        //gelen bir çok set var userıd kullanıcının emaili adı soyadı birde roller ekliyoruz onuda operationclaim'deki veritabanında çektigimiz rolleri array'e basıp rollere
        //ekliyoruz.
        #endregion
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
