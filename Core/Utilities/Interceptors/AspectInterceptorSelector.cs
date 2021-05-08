using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    #region Not
    //Burası çalıştırılmak istenen metodun üstüne bakıyordu (MethodInfo method) oradaki interceptorları buluyordu yani kısacası aspectleri buluyordu 
    //(GetCustomAttributes<MethodInterceptionBaseAttribute>(true);) ve onları çalıştırıyordu eger sen performans aspeticini buraya böyle 
    //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); bunun gibi eklersen bu senin mevcutta ve ilerde eklenicek butun metotlara eklenir örnegin biz burada
    //log'ı eklemişiz bu şu demek stajyer geldi metot yazdı ama loglamayı unutmuş böyle bir dert yok bütün metotlar burada loglanıyor. 
    #endregion
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
