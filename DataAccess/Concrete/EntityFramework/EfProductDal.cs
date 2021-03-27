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
    //Entity Framework Microsoft un bir ürünü bir ORM(object relational mapping) linq destekli çalışıyor amaç şu ORM demek veritabanındaki tabloyu sanki class mış gibi onunla
    //ilişkilenidirip bundan beri bütün operasyonları sql leri linq yaptıgımız bir ortam.ORM veritabanı nesneleri ile kodlar arasında bir ilişki kurup bag kurup veritabanı
    //işlerini yapma sürecidir.
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //Using => bu c#'a özel çok güzel bir yapı siz bir class'ı new'lediginiz zaman garbage collector belli bir zamananda düzenli olarak gelir ve bellekten onu atar
            //using içerisine yazdıgınız nesneler using bitince anında garbage collecter'e geliyor diyorki beni bellekten at diyor çünkü context nesnesi biraz pahalıdır.
            //Buradaki using IDispossable pattern implemantation of c#.
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity);//Burada diyorizki git veri kaynagından benim bu gönderdigim product'a bir tane nesneyi eşleştir (ekle).
                addedEntity.State = EntityState.Added;//Veri kaynagıyla ilişkilendirdikten sonra bunu yap diyoruz.Biz yukarıda referansı yakaladık.
                context.SaveChanges();//Yani ilk satırda referans'ı yakala sonra aslında o bir ekelenecek nesne ve buradada şimdi ekle diyoruz.
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //Tek data getierecek bu method.
        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);//Set ile şunu diyoruz DbSet lerinden Product'a baglan oda zaten Products tablosuna baglanıyor.
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                //Filter null ise Product'a yerleş ve oradaki tüm datayı ToList diyerek listeye çevir.Kısacası bu select*from Products çalıştırıyor ve onu list'e döndürüyüyor.
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
