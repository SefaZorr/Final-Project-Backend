using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //Entity Framework Microsoft un bir ürünü bir ORM(object relational mapping) linq destekli çalışıyor amaç şu ORM demek veritabanındaki tabloyu sanki class mış gibi onunla
    //ilişkilenidirip bundan beri bütün operasyonları sql leri linq yaptıgımız bir ortam.ORM veritabanı nesneleri ile kodlar arasında bir ilişki kurup bag kurup veritabanı
    //işlerini yapma sürecidir.

    //Bu EntityFramework kullanarak bir repository base'i oluştur demek.Bu yapı diyorki bana birtane TEntity(Tablo) ver bir tane de TContext context tipi ver ona göre
    //çalışıcam demektir bu yapı. Yani biz bundan sonra veritabanına yeni bir tablo ekledigimizde gidipte insert update delete getall mış neyse tum operasyonlar için bidaha
    //bidaha bişey yazmayalım diyoruz.
    public class EfEntityRepositoryBase<TEntity,TContext>: IEntityRepository<TEntity> 
        where TEntity:class,IEntity,new() 
        where TContext: DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //Using => bu c#'a özel çok güzel bir yapı siz bir class'ı new'lediginiz zaman garbage collector belli bir zamananda düzenli olarak gelir ve bellekten onu atar
            //using içerisine yazdıgınız nesneler using bitince anında garbage collecter'e geliyor diyorki beni bellekten at diyor çünkü context nesnesi biraz pahalıdır.
            //Buradaki using IDispossable pattern implemantation of c#.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);//Burada diyorizki git veri kaynagından benim bu gönderdigim product'a bir tane nesneyi eşleştir (ekle).
                addedEntity.State = EntityState.Added;//Veri kaynagıyla ilişkilendirdikten sonra bunu yap diyoruz.Biz yukarıda referansı yakaladık.
                context.SaveChanges();//Yani ilk satırda referans'ı yakala sonra aslında o bir ekelenecek nesne ve buradada şimdi ekle diyoruz.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //Tek data getierecek bu method.
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);//Set ile şunu diyoruz DbSet lerinden Product'a baglan oda zaten Products tablosuna baglanıyor.
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //Filter null ise Product'a yerleş ve oradaki tüm datayı ToList diyerek listeye çevir.Kısacası bu select*from Products çalıştırıyor ve onu list'e döndürüyüyor.
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
