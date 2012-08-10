using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///WcfService 的摘要说明
/// </summary>
public class WcfService
{

    #region 属性
    private static System.ServiceModel.ServiceHost _host;
    /// <summary>
    /// Wcf服务对象
    /// </summary>
    public static System.ServiceModel.ServiceHost Host
    {
        get { WcfServiceInit(); return WcfService._host; }
    } 
    #endregion

    /// <summary>
    /// 初始化Wcf服务
    /// </summary>
    public static void WcfServiceInit()
    {
        if (_host == null)
        {
            _host = new System.ServiceModel.ServiceHost(typeof(WcfServiceLibrary1.Service1));

            _host.Opened += delegate
            {
                WriteLog("Wcf服务启动成功！");
            };
            _host.Closed += delegate
            {
                WriteLog("Wcf服务停止成功！");
            };
        }
    }

    public static void Open()
    {
        WcfServiceInit();
        if (_host != null && _host.State != System.ServiceModel.CommunicationState.Opened)
        {
            _host.Open();
        }
        else
            WriteLog("Wcf服务已启动！");
    }

    public static void Close()
    {
        WcfServiceInit();
        if (_host != null && _host.State == System.ServiceModel.CommunicationState.Opened)
        {
            _host.Close();
        }
        else
            WriteLog("Wcf服务未启动！");
    }

    public static void WriteLog(string format, params object[] arg)
    {

    }

}