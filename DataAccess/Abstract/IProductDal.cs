using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //2 tane nesnemiz vardı product ve category yani iş yapacak classlarımızı oluşturacagımız zaman eger onun bir interface si yoksa her zaman onun interfacesini oluşturucaz.
    //Dal hangi katmana karşılık geldigini anlatır yani burada biz product entitysinin tablosunun dal ını yazıyor (Data Access Layer).Sektörde ya Dal yazılır yada Dao yazılır
    //(Data Access Object ).
    //Yani burası bizim Product ile ilgili veritabanında yapacagımız operasyonları içeren interface dir.
    public interface IProductDal:IEntityRepository<Product>
    { 
        
    }
}
