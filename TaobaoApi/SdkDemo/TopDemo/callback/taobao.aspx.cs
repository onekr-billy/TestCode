using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopCore;

namespace TopSdkMvcApplication.callback
{
    public partial class taobao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TopCore.TopUtility.AuthCallBack(true);

        }
    }
}