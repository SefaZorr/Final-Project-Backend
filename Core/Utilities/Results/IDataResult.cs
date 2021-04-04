using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult
    {
        //Data burada ürünler olur ürün olur void dışındakiler için result yazdık yani ürün için döner order için döner burada ama success ve message'ı IResult'tan alıcak.
        T Data { get; }
    }
}
