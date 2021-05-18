using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    #region Not
    //Attribute bir class ile ilgili aslında bilgi verme onu imzalama yöntemidir biz burada kısacası diyorizki bu class bir controller'dır o yüzden kendini ona göre yapılandır
    //diyoruz .net'e.(ApiController)
    //Route ise bize nasıl istekte bulunacaklar odur.Route bu istegi yaparken bu insanlar bize nasıl ulaşsın diyorki başına api yazsın 
    #endregion
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Not
        //Loose coupled => gevşek baglılık yani bir bagımlıgı var ama soyuta bagımlılık var dolayısıyla manager degişirse herhangi bir problem olmuyacak.Birden fazla
        //Manager olabilir projede farklı kurumlar için yazabiliriz.

        //Şimdi constructur'da IProductService istedik bagımlıgı azalatmak için ama şimdide elimizde somut referans yok peki nabıcaz burada karşımızı IoC Container denilen
        //bir tane yapı geliyor (Inversion of Control(degişimin kontrolü)).IoC'in mantıgı bir bellekteki bir kutu gibi bir liste gibi düşünün bu listenin içerisine ben oraya
        //new ProductManager(),new EfProductDal() böyle referanslar koyayım içine ondan sonra kim ihtiyaç duyuyorsa onu ona verelim. 
        #endregion

        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #region Not
        //Dolayısıyla Controller'da bu controller'da siz size yapılabilecek istekleri kodluyoruz yani onları tasarlıyoruz diyorizki insanlar bizim sistemimize bir HttpGet 
        //isteginde bulunabilirler mesela ismide Get olucak şekilde.
        //Bu şekil isim vermezsek patlıyoruz iki tane Get var diyor hangisi diyor. 
        #endregion
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            Thread.Sleep(1000);
            #region Not
            //Dependency chain var burada şuan bir bagımlılık zinciri var IProductService bir ProductManager'a ihtiyaç duyuyor ProductManager'da bir IProductDal'a ihtiyaç
            //duyuyor burada aslında bagımlı oldugu kişi ProductManager'in ta kendisidir.
            //Result Ok ile dönünce 200 ile dönüyor BadRequest ile dönünce 400 ile dönüyor. 
            #endregion
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        #region Not
        //Bir data eklemek için HttpPost requestinde bulunuyoruz.Yani post request yapılırsa.Datayı güncelleme ve silme içince post kullanabiliriz.Ama güncelleme için
        //HttpPut'u silme için HttpDelete 'ide kullanabilirsin.
        //Mesela instagramı açınca orda ana sayfayı yeniledin yukarıdan aşagı bi çektin orada aslında sen bir Get request yapıyorsun bana data ver diyorsun ama Post ise ben
        //sana data vericem onu al sistemine ekle demek post gönderi demek zaten.Bunu nerde nabıyoruz postman de bu bir zarf gibidir zarfın body sine gönderecegimiz data yı
        //ekliyoruz orda row'a geliyoruz veri tipi olarak json seçiyoruz çünkü bu bir restfull ve default olarak json çalışıyoruz ben sana bişeyler yazıcam o Json formatında
        //demek oluyor.Burada gönderecegimiz şey product'ın karşılıgı olan bir bilgiyi yollamaktan ibarettir. 
        #endregion
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
