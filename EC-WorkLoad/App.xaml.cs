using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EC_WorkLoad
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /*
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;//显式控制窗口关闭
                LoginWindow login = new LoginWindow();
                bool loginResult = login.ShowDialog() ?? false;
                if (loginResult)
                {
                    //成功，进入主界面
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;//取消显式控制窗口关闭
                }
                else
                {
                    Shutdown();
                }
            }
            catch (Exception ex)
            {

                var t = ex.Message;
            }
            
        }
        */
    }
}
