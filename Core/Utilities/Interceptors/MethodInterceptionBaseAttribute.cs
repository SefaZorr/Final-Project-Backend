using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        //Priority öncelik demek hangi attribute önce çalışsın önce validation sonra loglama öyle sıralama yapmak istersen kullanıyoruz.
        public int Priority { get; set; }
        //IInvocation bizim aslında çalıştırmak istedigimiz metodumuz oluyor.
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
