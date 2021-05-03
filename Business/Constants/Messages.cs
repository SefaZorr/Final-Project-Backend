using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    #region Not
    //Constant sabit demek proje sabitlerini bunun içerisine ama Northwinde özel proje sabitlerini bunun içerisine ama northwinde'e özel proje sabitleri mesala metinler
    //mesajlar enumlar burada kullanıyoruz.
    //Static sürekli newlemememk için kullanıyoruz Messages.  diyoruz uygulama hayatı boyunca tek instance oluyor bu tür yapıları static yaparız. 
    #endregion
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
