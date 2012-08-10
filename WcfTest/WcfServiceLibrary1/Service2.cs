using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfServiceLibrary1
{
    // 注意: 如果更改此处的类名 "Service2"，也必须更新 App.config 中对 "Service2" 的引用。
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public class Service2 : IService2
    {
        private ICalculatorCallback call;     

        public Service2()
        {
            call = OperationContext.Current.GetCallbackChannel<ICalculatorCallback>();
        }

        public void DoWork()
        {
            Thread.Sleep(5000);
            call.CallBackTestMethod();
        }
    }
}
