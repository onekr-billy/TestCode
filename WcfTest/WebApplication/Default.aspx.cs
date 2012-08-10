using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.ServiceReference;

namespace WebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.btn_text1.ServerClick += ASPxButton1_Click;
            this.Button1.Click += ASPxButton1_Click;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {

            var sc = new Service1Client();
            var result = sc.GetDataUsingDataContract(new CompositeType() { BoolValue = true, StringValue = "abc" });
            Response.Write(result.StringValue);

        }
    }
}
