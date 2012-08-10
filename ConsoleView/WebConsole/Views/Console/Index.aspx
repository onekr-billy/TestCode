<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/Scripts/View/Console/Index.aspx.js"></script>
    <h2>
        控制台管理程序</h2>
    <table style="width: 100%;">
        <tr>
            <td>
                输入命令：<input type="text" id="txt_cmd" />
                <input type="button" id="btn_exec_cmd" value="执行命令" />
                <input type="button" id="btn_start" value="启动服务" />
                <input type="button" id="btn_stop" value="停止服务" />
            </td>
        </tr>
        <tr>
            <td>
                <textarea style="width: 100%; height: 300px;" id="txt_log"></textarea>
            </td>
        </tr>
    </table>
</asp:Content>
