using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        //Memoryde çalıştıgımızı düşündügümüz için listemiz bizim için bir veritabanıdır.
        List<Product> _products;

        //Projeyi çalıştırdıgımızda bellekte bizim yerimize böyle bir ürün oluşturdu sanki bize bir oracle,sql,posgre den geliyormuş gibi simüle ediyoruz.
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        //Burada product.Remove() çalışmaz çünkü bunu çalıştırıken new product diyip gönderecegiz böyle oluncada yeni bir bellek adresi alış olucak oda listede olmayabilir.
        //Parametre olarak string int vs alsaydı direk silerdi çünkü bunlar deger tiplerdir.Referans tip böyle silemeyiz.
        public void Delete(Product product)
        {
            //LINQ - Language Integrated Query
            //Bu LINQ kullanmazsak foreach ile tek tek products'ı kontrol etmemız gerekecek.
            //SingleOrDefault tek bir eleman bulmaya yarar bu products'ı tek tek dolaşır.P burada tek tek dolaşırken verdigimiz takma isim.Yanı bu kod  foreach'i kullanmadan
            //yapıyor. 
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
           
        }

        public List<Product> GetAll()//Veritabanındaki datayı business'e veriyoruz.
        {
            return _products;
        }


        //Buradada yine delete'de oldugu gibi güncellenecek referansı bulmamız lazım.
        public void Update(Product product)
        {
            //Gönderdigim ürün id'sine sahip olan listedeki  ürünü bul demektir.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
