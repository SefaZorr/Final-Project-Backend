using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        #region Not
        //Bu (context) şu demek Product için bir dogrulama yapıcam çalışacagım tipte parametreden gelen product'dır diyoruz.Şimdi bunu dogrulucaz neyi kullanarak dogrulacz
        //ProductValidator kendi yazdıgımız ile dogrulayacagız o kuralları biz kendimiz yazdık.Eger sonuç geçerli degilse. 
        #endregion
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
