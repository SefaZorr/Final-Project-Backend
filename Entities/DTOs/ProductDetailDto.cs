using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    //DTO(Data Transformation Object) bir e-ticaret sitesine girdiginde sen bir ürünün listesinde ilişkisel tablolardaki datalarıda görüyorsun ürünün ismide yazıyor 
    //categori ismide yazıyor.
    //IDto sen bir Dto sun diyoruz neden bunu yapıyoruz çünkü kafasına göre gidip kendini context'e eklemesin diye.
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
