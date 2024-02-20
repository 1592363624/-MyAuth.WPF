using System;
using System.Diagnostics;
using System.Windows;
using AutoUpDate.Utiles;

namespace AutoUpDate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            //Control.关闭进程("ShellToolBox");
            InitializeComponent();
            DataContext = StaticDataSource.downloadItem;
            StarDownload.Initial();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            button_UpData.IsEnabled = false;
            button_Cancel.IsEnabled = true;
            await StarDownload.StarDown();
            button_UpData.IsEnabled = true;
            button_Cancel.IsEnabled = false;
            button_Cancel.Content = "暂停";
        }


        bool flg = false;
        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (flg == false)
            {
                StaticDataSource.downloadService.Pause();
                button_Cancel.Content = "继续";
                flg = true;
            }
            else
            {
                StaticDataSource.downloadService.Resume();
                button_Cancel.Content = "暂停";
                flg = false;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (StaticDataSource.接收到的参数个数 > 1)
            {
                // 获取所有正在运行的进程列表
                Process[] processes = Process.GetProcesses();

                // 遍历所有进程
                for (int i = 0; i < processes.Length; i++)
                {
                    // 获取进程名称
                    string processName = processes[i].ProcessName;
                    // 判断进程名称是否与指定名称匹配
                    if (processName == StaticDataSource.上级调用程序名)
                    {
                        // 关闭进程
                        processes[i].Kill();
                    }
                }
                MessageBox.Show("更新程序即将关闭,请重启来使用更新版本程序!");
            }

        }
    }

}
