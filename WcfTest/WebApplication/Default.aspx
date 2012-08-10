<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JavaScripts/require.js" type="text/javascript"></script>
    <script src="JavaScripts/requirejs.config.js" type="text/javascript"></script>
    <script src="Default.aspx.js" type="text/javascript"></script>
</head>
<body>
    <form runat="server">
    <div>
    <input type="button" id="btn_text1" runat="server" value="abc"  />
        <asp:Button Visible="false" ID="Button1" runat="server" Text="后台直接访问 Wcf" /><br />
        <input type="button" id="t1" value="jQuery GET 跨域访问" /><br />
        <input type="button" id="t2" value="本站代理 通过后台接口 " /><br />
        <input type="button" id="t3" value="本站代理 通过REST GET" /><br />
        <input type="button" id="t4" value="本站代理 通过REST POST" /><br />
        <input style="display: none;" type="button" id="t5" value="本站代理 通过REST PUT" /><br />
        <input style="display: none;" type="button" id="t6" value="本站代理 通过REST DELETE" /><br />
    </div>
    </form>
</body>
</html>
