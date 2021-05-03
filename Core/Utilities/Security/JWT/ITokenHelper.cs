using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    #region Not
    //Burası ne yapıcak kullanıcı kullanıcı adı ve şifre giricek girdikten sonra apiye gelicek api ye geldikten CreateToken çalışacak eger dogruysa ilgili kullanıcı için
    //veritabına gidicek veri tabanından bu kullanıcın clamilerini buluşturacak orada bir jwt üretecek ve geri gelecek oradan.
    //ITokenHelper ilgili kullanıcı için ilgili kulanıcının claimlerini içerek bir token üretecek.
    #endregion
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
