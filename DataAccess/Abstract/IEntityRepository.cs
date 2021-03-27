using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //Generic constraint => Generik kısıt demek.T yi kısıtlayacağız.T bir referans tip olmalı ve T ya IEntity olabilir yada IEntity den implemente eden bir nesne olabilir.
    //new() demek newlenebilir demek IEntity interface oldugu için new lenemez oldugu için IEntity de yazamayız artık.
    //Class demek class demek degil burada referans tip olabilir demek.
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        //Tüm datayıda getirebilir ama filtre vererek de getirebilir.Expression dedigimiz ise mesela linq yaparken (p=>p.categoryid==3) dedimiz yapı bir expression olarak 
        //geçiyor.Yani bu yapıyla gidipte kategoriye göre getir ürünün fiyatına göre getir için ayrı ayrı metotlar yazmamız gerekmiyecegiz.Filtre=null filtre vermeyebilirsin
        //demek.Yani biz şöyle bir mantık kurucaz filtre vermemişse tum datayı istiyor filtre verirse filtreyelip vericek.
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        //Tek bir datayı getirmek için genellikle bir sistemde bir şeyin detayını gitmek bankacılık sisteminde bir kişilerin kredilerini listeleyip basıp onun detayını
        //gitmek için yani tek kredinin detayına veya bankacılık uygulmasında hesaplarımız var hesaplar liste olarak geliyor biz ordan bir tane hesaba tıklayıp detaylarına
        //gidiyoruz.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity); 
    }
}
