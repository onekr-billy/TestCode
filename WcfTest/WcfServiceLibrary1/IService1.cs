using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceLibrary1
{
    // 注意: 如果更改此处的接口名称“IService1”，也必须更新 App.config 中对“IService1”的引用。
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = WcfConfig.GETDATA)]
        CompositeType GetData(string value);

        [OperationContract()]
        [WebInvoke(Method = "POST",
            UriTemplate = "/PostData/{value}",
            RequestFormat = WebMessageFormat.Json)]
        string PostData(string value, CompositeType entity);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/PostDatae",
            RequestFormat = WebMessageFormat.Json)]
        string PostDatae(CompositeType entity);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "/PutData/{value}",
            RequestFormat = WebMessageFormat.Json)]
        CompositeType PutData(string value, CompositeType entity);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
            UriTemplate = "/DeleteData/{value}",
            ResponseFormat = WebMessageFormat.Json)]
        bool DeleteData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract(IsOneWay = true)]
        void TestMethod(string str);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        CompositeType WebGetT(string str);



        // 任务: 在此处添加服务操作
    }

    // 使用下面示例中说明的数据协定将复合类型添加到服务操作
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
