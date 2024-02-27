using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using LoginLauncher.Model.MyAuth;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using LoginLauncher.Views;
using LoginLauncher.ViewModels;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;



namespace LoginLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string? 检测更新msg { get; set; } = "当前版本已是最新版本!";
        public string? 检测更新msg颜色 { get; set; } = "#FF16C675";

        public MainWindow()
        {
            InitializeComponent();
            myListBox.SelectedIndex = 0;//默认展示第一个登录页面
            image.Visibility = Visibility.Visible;
            DataContext = this;
            qq_image.DataContext = DataSource.MWVM;
            txt_cs.DataContext = DataSource.MWVM;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterFrame.Navigate(new System.Uri("Views/RegisterView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Window_初始化完成()
        {
            string ConnectMsg = Myauthweb.Connect();
            if (ConnectMsg != "服务器正常")
            {
                MessageBox.Show(ConnectMsg);
                Process.GetCurrentProcess().Kill();
            }
            检测更新msg = Myauthsoft.CheckUpdate();
            if (检测更新msg == "检测到有新版本")
            {
                检测更新msg颜色 = "Red";
                Core.调用程序exe("AutoUpDate.exe", "来自Shell应用程序调用 LoginLauncher");
            }
            //else
            //{
            //    调用程序exe("ShellToolBox.exe", "来自Shell应用程序调用 LoginLauncher");
            //}
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // 关闭应用程序
                Application.Current.Shutdown();
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            RegisterFrame.Navigate(new System.Uri("Views/LoginView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataSource.ComputerUserName = Environment.UserName;
            // 读取JSON文件
            string filePath = "appsettings.json";
            string ComputerUserName = "";
            string jsonContent = File.ReadAllText(filePath);

            // 解析JSON内容
            JObject json = JObject.Parse(jsonContent);

            string Device_info = json["Info"]["Device_info"].ToString();
            string Device_code = json["Info"]["Device_code"].ToString();
            DataSource.Device_info = Device_info;
            DataSource.Device_code = Device_code;
            try
            {
                ComputerUserName = json["Info"]["ComputerUserName"].ToString();
            }
            catch (Exception)
            {

                json["Info"]["ComputerUserName"] = "null";
                // 将修改后的JSON内容写回文件
                File.WriteAllText(filePath, JsonConvert.SerializeObject(json, Formatting.Indented));
            }
            //测试窗口效果的时候  true   发布前改为  false
#if false
            FirstInitView fv = new FirstInitView();
            this.Hide();
            fv.ShowDialog();
            this.Show();
#endif

            if (ComputerUserName != DataSource.ComputerUserName || Device_info == "null" || Device_code == "null")
            {
                FirstInitView firstInitView = new FirstInitView();
                this.Hide();
                firstInitView.ShowDialog();
                this.Show();
            }

            Window_初始化完成();
        }

        private void ListBoxItem_Selected_About(object sender, RoutedEventArgs e)
        {
            RegisterFrame.Navigate(new System.Uri("Views/AboutView.xaml", UriKind.RelativeOrAbsolute));
        }
    }


}
