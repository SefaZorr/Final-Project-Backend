using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Contextde ürünler ile category leri join et demek.
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto { ProductId = p.ProductId, ProductName = p.ProductName, CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock };
                return result.ToList();
            }
        }
    }
}
