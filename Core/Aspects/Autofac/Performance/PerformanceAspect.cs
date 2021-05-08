using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    #region Not
    //Bunun olayıda basit bir tane timer(Stopwatch) koyuyoruz bu metot ne kadar sürücek diye.Metodun önünde(OnBefore) kronometreyi çalıştırıyoruz. Metot bittigindede ise geçen
    //süreyi hesaplıyoruz (_stopwatch.Elapsed.TotalSeconds > _interval) dikkat edin bu performanceAspect'i çagırınca interval veriyoruz interval diyorizki bu metodun çalışması
    //interval'de verilen süreyi geçerse beni uyar diyoruz.Biz anlayacazki özellikle performansta zafiyetine sebep olan metot hangisi bulabilecez.Eger intervalde verilen
    //süreyi geçince burada console'a log olarak yazmışız siz artık burada mail mi gönderirsiniz log mu alırsınız size kalmış. 
    #endregion
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
