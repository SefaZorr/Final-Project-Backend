using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı degil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) 
        {
            #region Not
            //Reflection=>Reflection çalışma anında bir şeyleri çalıştırabilmemizi saglıyor örnegin biz bir şeyi new liyoruz ya ama siz new leme işini çalışma anında yapmak
            //istiyorsunuz diyelim Activator.CreateInstance bu bir reflection'dır o ProductValidator'ı bir instance'ını oluştur sonra diyor ProductValidatar'un çalışma tipini
            //bul diyor (Oda AbsractValidator<Product> yani product'tır).Yani product validator'ın base'ini bul sonra onun generic argümanlarından ilkini bul zaten bu ilki
            //biliyoruz.Sonra diyorizki bunun parametrelerini bul diyoruz (Paremetrelerini bul demek ilgili metodun paremetrelerini bul demek) (invocation metot demek unutma
            //metodun parametrelerini bak entityType 'a denk gelen yani Product'a denk gelen kısacası diyorki validator'ın tipine eşit olan parametreleri git bul diyor birden
            //fazla parametrede olabilir birden fazla Validation'da olabilir) onu bul diyor ve her birini tek tek gez ValidationTool kullanarak validate et diyor burası. 
            #endregion
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
