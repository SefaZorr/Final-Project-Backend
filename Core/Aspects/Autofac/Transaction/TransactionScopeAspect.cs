using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    #region Not
    //IInvocation metot demek senin metodun burada bir şablon oluşturuyoruz.invocation.Proceed(); bizim metodumuzu çalıştır demek.Exception oldugunda transaction.Dispose()
    //yap diyoruz.Throw et kullanıcıyı uyarıyor işlem başarısız diye. 
    #endregion
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
