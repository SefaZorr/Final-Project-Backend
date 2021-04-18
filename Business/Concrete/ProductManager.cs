using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    #region Not
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
    #endregion

    #region Not
    //Validation (Dogrulama) => Obje veya nesnenin iş kurallarına dahil etmek için yapısal olarak dogru olup olmadıgı kontrol etmeye validation denir.Örnegin bir sisteme kayıt
    //olurken minimum şu kadar karakter olmalı şifre şöyle olmalı gibi bu gibi kurallar'a validation diyoruz. 
    //İş kuralı => ise bizim iş gereksinimlerinize ihtiyaçlarımıza uygunluktur.Örnegin ehliyet alırken bir kişiye ehliyet verip vermeme kontrolu burasıdır ilk yardımdan 70 
    //almış mı gibi kuralları burada yapıyoruz yada bir bankada kredi verirken kişinin o krediye uygun olup olmadıgının kontrolu burada yapılıyor finansal puanına bakmak 
    //bunlar iş kodu ama ilgili nesyneyi buraya eklenmesini istedigimiz nesnenin yapısıyla ilgili olan şeyler validation'dır.
    #endregion

    #region Not
    //Cross Cutting Concerns=> Türkçeye uygulamayı dikine kesen ilgi alanları diye geçer örnegin loglama biz farklı katmamlarda bunu yapabilirz arayüz log'u iş logu diye felan
    //bunlar her yerde kullanıldıgı için loglama,cache(arayüz cache,data cache,business cache) veya dogrulama aynı şekilde yani biz bütün farklı katmanlarda bunların farklı
    //versiyonlarını yapıyoruz örnegin bu yaptıgımız dogrulamanın arayüz versiyonuda var orayada entegre olabiliyor fluent validation dolayısıyla bunlara Cross Cutting Concern
    //deniyor nedir bunlar;Log,Cache,Transaction yönetimi,Authorization.Dolayısıyla bu validasyonda bir cross cutting concerns ise onu core'da o şekilde ele alabiliriz. 
    #endregion
    public class ProductManager : IProductService
    {
        //Burada artık iş kodlarını yazıyoruz.
        //Bir iş sınıfı başka sınıfları new'lemez.

        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal; 
        }
        #region Not
        //Business kodlar buraya yazılır urunu eklemeden önce kodlarını buraya yazarız herşey geçerli ise ürünü ekleriz degilse eklemeyiz.

        //[LogAspect] => AOP bir metodunun önünde bir metodun sonunda bir metod hata verdiginde çalışan kod parçacıklarını bir AOP mimarisiyle yazıyoruz yani Business içinde
        //business yazıyoruz.Biz bu Add içerisinde yok loglama yönetimi yok hata yönetimi yok transaction yönetimi yok performans yönetimi yok cache yönetimi yok validasyon
        //yönetimi hepsini bunun içine koyarsak burası çorba olur.Bu teknik spring boot uygulamalarında default olarak vardır.
        //[Validate]=> ürün eklenecek kuralları uygula.
        //[Cache]=>Cache'den çalış
        //[RemoveCache]=>ürün eklenirse bunu cache'den uçur.
        //[Transaction]=>Hata olursa geri al demek.
        //[Performance]=>Bu method benim sistemde performans olarak izledigim bir yapıdır eger çalışması 5 sn geçerse beni uyar. 
        #endregion

        #region Not
        //AOP=>Örnegin siz metotlarınızı loglamak istiyorsunuz bir metot ne zaman loglanır ya başında ya sonunda veya hata verdiginde işte sen uygulamanın başında sonunda
        //hata verdiginde çalışmasını istedigin kodların varsa onları AOP yöntemiyle güzel güzel design edebilirsin dolayısıyla uygulamada her yerde try catch gibi kodlar
        //yazmak zorunda kalmazsınız yada her yerde log log demek zorunda kalmazsınız işte bu yönteme interception deniyor.(interceptors(araya girme metodun başı sonu gibi)).
        
        //Attribute niye kullanıyoruz attribute şu işe yarıyor sen bir kodu çagıracagın zaman Add'i çarıgracagın zaman diyorsinki git üstüne bak bakalım belli kurala uyan
        //attribute'lar varmı varsa gidip onları çalıştırıyorsun.Kısaca attribute'lar property'lere field'lara metotlara classlara anlam yüklemek için kullandıgımız
        //yapılardır.
        #endregion

        //Bu metodu dogrula ProductValidator'u kullanarak.
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        #region Not
        //Mesela burda kurallara bakıyor yetkisi felan varmı diyelimki geçti o zaman bana ürünleri verebilirsin diyor dataaccess'e gidip.
        //Bu saat 22 de her gün sistemi kapatıyor ürünlerin listelenmesini kapatıyor mesela. 
        #endregion
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
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
