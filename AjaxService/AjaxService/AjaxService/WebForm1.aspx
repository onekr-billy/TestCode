<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AjaxService.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.js"></script>
</head>
<body>
    <script type="text/javascript">
        $(function () {

            $("#btnGet").click(function () {
                $.get("AjaxService.ashx", {
                    act: "GetId",
                    args: "12,zhangbin"
                }, function (html) {
                }, "html");
            });

            $("#btnPost").click(function () {
                $.post("AjaxService.ashx", {
                    act: "SaveId",
                    args: "{a:1}"
                }, function (html) {
                }, "html");
            });

        });
    </script>
    <input type="button" id="btnGet" value="GET" />
    <input type="button" id="btnPost" value="POST" />
</body>
</html>
