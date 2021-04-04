using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    //Constant sabit demek proje sabitlerini bunun içerisine ama Northwinde özel proje sabitlerini bunun içerisine ama northwinde'e özel proje sabitleri mesala metinler
    //mesajlar enumlar burada kullanıyoruz.
    //Static sürekli newlemememk için kullanıyoruz Messages.  diyoruz uygulama hayatı boyunca tek instance oluyor bu tür yapıları static yaparız.
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
    }
}
