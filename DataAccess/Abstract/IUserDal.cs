using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    #region Not
    //Ekstradan bir tane GetClaims operasyonu koyduk neden burda bir tane Join atıcaz.Zaten kullancının eklenmesi silinmesi zaten olucak ama aynı zamanda veritabanından
    //claimlerini çekmek için yazdık. 
    #endregion
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
