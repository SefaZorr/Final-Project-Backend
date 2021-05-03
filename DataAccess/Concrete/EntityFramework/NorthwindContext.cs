using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    #region Not
    //Context demek veritabanı ile kendi classlarımızı ilişkilendirdiğimiz class'ın ta kendisidir mesela veritabanındaki Product ile buradaki product ilişkilendirmek.Kısaca
    //Context Db tabloları ile proje classlarını bağlamak. 
    #endregion
    public class NorthwindContext:DbContext
    {
        #region Not
        //Bu method (OnConfiguring) bizim projemiz hangi veritabanı ile ilişkiliyi belirtecegimiz yerdir. 
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region Not
            //Sql server kullanıcaz diyoruz burada nasıl baglanacagımızı belirtmemiz gerekiyor.@ koymak ters slashı \ normal ters slash olarak algıla demek yoksa iki tane
            //ters slash koymamız gerekirdi. 
            #endregion
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        #region Not
        //Şimdi veritabanımızın ne oldugunu söyledik yukarıda ama benim hangi nesnem hangi nesneye karşılık gelicek bunuda DbSet nesnesi ile yapıyoruz.Mesela bizdeki
        //Product'ı Products'a bagla diyoruz. 
        #endregion
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
