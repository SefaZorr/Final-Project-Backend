using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak Class Kalmasın (Herhangi bir sınıf bir yerden kalıtım yada interface implementasyonu almalıdır.
    //Böyle IEntity ile işaretlememizin avantajı bize IEntity mesela Category nin referansını tutabiliyor.
    public class Category:IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
