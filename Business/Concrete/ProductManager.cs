using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    //Bizim önce DataAccess sonra Business ve WebApi katmanı gelicek WebApı katmanı bizim mesela arayüz olarak angular console ios yada android(bunlara biz client diyoruz 
    //her biri uygulamaın bir müşterisi) uygulamları bizimle görüşebilmesi için biz buraya bir katman daha olşuşturuyoruz bu katmanın adı WebApı.WebApı nabıyor örnegin
    //bizim yazdıgımız .net c# kodu ios için android için flutter için angular için bir anlam ifade etmiyor console,webform,wpf,xamarin için ediyor çünkü bunlar .net projesi 
    //WebApı kullanmadanda iletişim kurabilirz ama bir angular bir ios,react,bir view bir android uygulaması bizim yazdıgımız kodu (.net) anlamaz o yüzden bunu anlasın diye 
    //yazılım dünyasında bir standart var WebApi olarak yapıyoruz .net projelerinde bu neydi bir Restful dedigimiz aslında bir format ile çalışan genellikle bu format JSON 
    //olan HTTP istekleri üzerinden yapılan sürecin ta kendisidir.Farklı uygulamalarda bu WebApı 'ya gelmeden önce oluşturmadan önce bu farklı clientlarla çalışacagımız 
    //zaman onların bizim yazdıgımız uygulamaya Restfull service istek(request-response) yapmasını bekliyoruz istek yaptıkları zaman istege Request deniyor mesela ben
    //şu kategorideki ürünleri istiyorum demek biz ona yanıt olarak response veriyoruz işte bu request ve response sürecini daha iyi yönetebilmek için profesyonel bir alt
    //yapı yazacagız sisteme.Tabiki bu özellikle bizim burada bu iş katmanında yaptıgımız işlemlerin sonucunun ne olduguyla ilgilenecek yani tam olarak şunu demek istiyoruz
    //biz bir istekte bulunan kişiye arkadaşımsın sen Add yapmak istedin ve senin yaptıgın işlemin sonucunda işlem başarısız oldu ben ekleyemedim çünkü şundan dolayı ekleye
    //medim gibi veya arkadaşım sen eklemek istedin ve bende onu ekledim yaptıgın işlem başarılı haberin olsun şeklinde verdigimiz yapıları burada oluşturuyor olucagız.
    //Yani mesela aşagıdaki Add,GetAll felan daha profesyonel hale getiriyor olacagız.Ve bunu bir kere yazıcaz bidaha yazmıyacagiz.
    public class ProductManager : IProductService
    {
        //Burada artık iş kodlarını yazıyoruz.
        //Bir iş sınıfı başka sınıfları new'lemez.

        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //Business kodlar buraya yazılır urunu eklemeden önce kodlarını buraya yazarız herşey geçerli ise ürünü ekleriz degilse eklemeyiz.
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //Mesela burda kurallara bakıyor yetkisi felan varmı diyelimki geçti o zaman bana ürünleri verebilirsin diyor dataaccess'e gidip.
            //Bu saat 22 de her gün sistemi kapatıyor ürünlerin listelenmesini kapatıyor mesela.
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()  
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
