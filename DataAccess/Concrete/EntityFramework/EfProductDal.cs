using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Burada EfEntityRepositoryBase çözdügümüz zaman artık IProductDal'da kızmıyor çünkü IProductDal'da bulunan operasyonlar zaten EfEntityRepositoryBase'de var. IProductDal
    //gereksiz gibi gelebilir ama degil çünkü business'ta kullanıyoruz bunu burası sadece Ef ile ilgili kısım ileride Depper'a geçebiliriz o yüzden silemeyiz bunu.
    public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>, IProductDal
    {
        
    }
}
