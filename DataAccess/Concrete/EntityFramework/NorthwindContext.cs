using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context demek veritabanı ile kendi classlarımızı ilişkilendirdiğimiz class'ın ta kendisidir mesela veritabanındaki Product ile buradaki product ilişkilendirmek.Kısaca
    //Context Db tabloları ile proje classlarını bağlamak.
    public class NorthwindContext:DbContext
    {
        //Bu method (OnConfiguring) bizim projemiz hangi veritabanı ile ilişkiliyi belirtecegimiz yerdir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Sql server kullanıcaz diyoruz burada nasıl baglanacagımızı belirtmemiz gerekiyor.@ koymak ters slashı \ normal ters slash olarak algıla demek yoksa iki tane
            //ters slash koymamız gerekirdi.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        //Şimdi veritabanımızın ne oldugunu söyledik yukarıda ama benim hangi nesnem hangi nesneye karşılık gelicek bunuda DbSet nesnesi ile yapıyoruz.Mesela bizdeki
        //Product'ı Products'a bagla diyoruz.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
