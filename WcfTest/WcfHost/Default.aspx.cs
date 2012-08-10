using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using System.Threading;
using System.Configuration;
using System.Collections.Specialized;

namespace WcfHost
{
    public partial class _Default : System.Web.UI.Page
    {
        //public static ServiceHost host;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (host == null || host.State == CommunicationState.Faulted)
            //{
            //    host = new ServiceHost(typeof(WcfServiceLibrary1.Service1));
            //    host.Open();
            //}
            //if (host.State != CommunicationState.Opened)
            //    host.Open();

            NameValueCollection obj = (NameValueCollection)ConfigurationSettings.GetConfig("myconfigs/myconfig1");


            return;
            while (true)
            {
                Response.Write(@"<div style='display:none'>流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流
            流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流
                流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流流</div>");
                WcfService.Open();
                Response.Write(string.Format("<p>Wcf服务状态为:{0}</p>", (CommunicationState)WcfService.Host.State));
                Response.Flush();
                Thread.Sleep(1000);
            }
        }
    }
}
