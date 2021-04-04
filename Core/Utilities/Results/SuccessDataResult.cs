using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Burada işlem sonucunu default olarak true vericez.
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message):base(data,true,message)
        {
                
        }

        public SuccessDataResult(T data):base(data,true)
        {

        }

        //default demek dataya karşılık geliyor mesela türü int ise int in defaultı geç demek.Yani çalıştıgımız T'nin default'u demek.List'in default'u Null dır null döner.
        public SuccessDataResult(string message):base(default,true,message)
        {

        }

        public SuccessDataResult():base(default,true)
        {

        }
    }
}
