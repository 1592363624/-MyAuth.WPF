using System.Diagnostics;
using System.Windows;
using ShowMeTheXAML;

namespace AutoUpDate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// <!--接收处理调用本程序传递进来的参数-->
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //ShowMeTheXAML.MSBuild的引用
            XamlDisplay.Init();
            base.OnStartup(e);
            //ShowMeTheXAML.MSBuild的引用

            //因为调试启动会没有参数,所以不执行,用于在调试模式正常执行
            if (!StaticGlobalVar.IsDebugMode)
            {
                base.OnStartup(e);

                // 获取命令行参数
                string[] commandLineArgs = e.Args;
                StaticDataSource.接收到的参数个数 = commandLineArgs.Length;
                // 处理命令行参数
                if (StaticDataSource.接收到的参数个数 > 0)
                {
                    if (commandLineArgs[0] != "来自Shell应用程序调用")
                    {
                        MessageBox.Show("请使用相应的程序调用启动本更新程序。", "更新程序启动失败", MessageBoxButton.OK, MessageBoxImage.Error);
                        // 强制关闭应用程序
                        Process.GetCurrentProcess().Kill();
                    }
                    //MessageBox.Show("接收到的参数：" + receivedArgument, "命令行参数");
                }
                else
                {
                    MessageBox.Show("请不要直接启动本更新程序。", "更新程序启动失败", MessageBoxButton.OK, MessageBoxImage.Error);
                    // 强制关闭应用程序
                    Process.GetCurrentProcess().Kill();
                }

                if (StaticDataSource.接收到的参数个数 > 1)
                {
                    StaticDataSource.上级调用程序名 = commandLineArgs[1];
                }


            }


        }
    }
}
