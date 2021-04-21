using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        #region Not
        //Parametre ile gönderilen iş kurallarından başarısız olanı Business'a haber ediyoruz diyorizki business kardeş şu logic hatalı gibi. 
        #endregion
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}
