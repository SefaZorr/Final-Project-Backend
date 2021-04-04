using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangiç.
    //Voidleri IResult yapısı ile süslüyor olacagız.
    //2 tür yaklaşım var burada istersek try catch ile istersek bu şekilde bu yönetimi yapabiliriz.
    //Amaç bizim apilerimizi veya uygulamamızı kullanacak kişileri doğru yönlendirmek.
    //Mesela Add void tir Success diyecek ki yapmaya çalıştıgın ekleme işi başarılı veya başarısız.Message is yapmaya çalıştıgın işlem başarılı kullanıcıya ürün eklendi gibi bir bilgilendirme verme.
    //Bu Results bir kere yazacagımız ve bundan sonra projede defalarca kullanabilecegimiz bir ortam sagladı bize.
    public interface IResult
    {
        //Get demek sadece okunabilir demek set yazmak için demek.
        bool Success { get; }
        string Message { get; }
    }
}
