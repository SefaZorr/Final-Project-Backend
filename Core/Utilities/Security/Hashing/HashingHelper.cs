using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        #region Not
        //Kısacası biz bir password vericez dışarıya şu iki degeri çıkaracak yapı tasarlıyacagız passwordHash ve passwordSalt.Burada çok basit bir sistemden yararlnıcaz .net
        //in Kriptografi sınıflarıdan yararlanıcaz ondan yararlanarak algoritmamızı seçip o algortimaya göre hash degerimizi oluşturucaz bunuda disposable patterns ile (using)
        //yapıcaz.hmac bizim kriptografı sınıfında kullandıgımız class a denk geliyor biz burada SHA512 algoritmasını kullanıyor olucaz.passwordSalt dedigimiz hmac de bulunan
        //key degerimiz olucak passwordHash te hmac içerisinde bulunan computehash() isimli metodu ile gerçekleştiricez yukarıda verilen password'u vericez Encoding.UTf8
        //ile bir stringin byte karşılıgını alıyoruz.Kısacası bu yazdıgımız kod verdigimiz bir password degeririnin salt ve hash degerini oluşturmaya yarıyor.
        #endregion
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        #region Not
        //Password hashini dogrula demek.Bu metot burada out gerek yok çünkü bu degerleri biz vericez diyecezki benim elimde böyle bir şifre var adam 1234@1234 girdi sen
        //bunu bu kullancının girdigi password'ü yine aynı algoritmayı kullanarak hasheleseydin karşına böyle birşey çıkarmıydı çıkmazmıydı buradaki parametler olarak
        //verilen passwordHas ve passwordSalt veritabanımızdaki hash olucak bu hash ile kullancının gösterdigi password in oluşacak hashe ini karşılaştırıyoruz eger eşit ise
        //true eşit degilse false diyecegiz.Buradaki passoword kullanıcının sisteme tekrar girmek için kullandıgı parola. 
        #endregion
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    } 
                }
                return true;

            }
        }
    }
}
