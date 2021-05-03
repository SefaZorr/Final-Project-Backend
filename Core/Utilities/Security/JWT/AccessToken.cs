using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    #region Not
    //AccessToken ise ilk derste bahsettigimiz kullanıcı sisteme istekte bulunurken eger yetki gerektiren birşey ise elinde bir tane token vardı o token'da bu paketin
    //içine koyar ve gönderir buna AccessToken deniyor. AccessToken oluşturdugumuz zamanda görücez böyle anlamsız karakterlerden oluşan bir anahtar degeridir.Expiration tokenın
    //bitiş zamanını verdigimiz yer.Token jwt degerinin ta kendisidir.Kullanıcı api üzerinden postmen üzerinden kullanıcı adı ve parolasını vericek bizde ona bir token ve
    //ne zaman sonlanacagının süresini veriyor olacagız.
    #endregion
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
