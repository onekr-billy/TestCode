using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.ServiceModel.Web;

namespace WcfServiceLibrary1
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [JavascriptCallbackBehavior(UrlParameterName = "callback")]
    public class Service1 : IService1
    {

        public CompositeType GetData(string value)
        {
            return new CompositeType() { StringValue = string.Format("Get Result: {0}", value), BoolValue = true };
        }

        public string PostData(string value, CompositeType entity)
        {
            //Thread.Sleep(10000);
            return string.Format("Post Result: {0}", value);
        }

        public string PostDatae(CompositeType entity)
        {
            return string.Format("Post Result: {0}", "sdf");
        }

        public CompositeType PutData(string value, CompositeType entity)
        {
            return new CompositeType() { StringValue = string.Format("Put Result: {0}", value), BoolValue = true };
        }

        public bool DeleteData(string value)
        {
            return true;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void TestMethod(string str)
        {
            Thread.Sleep(5000);
            var db = new bbHomeEntities();
            var querytxt = from a in db.base_t_member where a.membNo.Equals(1) select a;
        }

        public CompositeType WebGetT(string str)
        {
            return new CompositeType() { BoolValue = true, StringValue = "abc" };
        }

    }
}
