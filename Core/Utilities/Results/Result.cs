using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult 
    {

        public Result(bool success, string message):this(success)
        {
            //Hani Message ve Success getter idi hani set edilemezdi neden getter ReadOnly dir readonly ler contructer'da set edilebilir o yüzden biz constructure dışında
            //set etmiyecegiz o yüzden bu yapıyı kullandık getter dedik setter koymadık oraya ki adam tamamen constructure yapısıyla kullansın bunu ama aşagıya setter'da
            //koyabilirdek ama programcı burada olayı kafasına göre kodlayabilirdi biz burada sınırlandırıyoruz burada biz diyorizki burada biz Northwind firması olarak
            //projelerimizde başarı dönüşümlerini contructure ile yapıcaz o yüzden adam kafasına göre result.success yapmasın kodların okunurlugu standart olsun diye bu yapıyı
            //standardize ediyoruz.
            //Biz şeyi düşündük adam belki her seferinde message girmek istemiyor sadece işlem başarılımı başarsızımı onu istiyor o yüzden burada 2 farklı contructure kullandık
            //alltaki constructure Success'i set ediyor yani bu çalıştıgında aşagıdaki çalışması için ve onu kullanabilmesi için yukarıdakine diyoruz ki this(this c# ta 
            //bulunulan sınıfı temsil eder) kullandık yani result tek parametreli contructure ına success'ı yolla böylece 2 parametreli olanı gönderirsen 2 side çalışır.
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
