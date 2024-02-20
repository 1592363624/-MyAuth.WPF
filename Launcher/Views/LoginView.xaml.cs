using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LoginLauncher.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Page
    {

        public LoginView()
        {
            InitializeComponent();
            DataContext = DataSource.loginData;
            Loaded += RegisterView_Loaded;
        }


        private void RegisterView_Loaded(object sender, RoutedEventArgs e)
        {
            // 创建一个平移动画
            TranslateTransform trans = new TranslateTransform();
            this.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(550, 0, TimeSpan.FromSeconds(0.3));

            // 指定动画的完成事件
            //anim.Completed += (s, _) => this.Close();

            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string result = DataSource.login.SoftLogin();
            if (result == "登录成功")
            {
                Window mainWindow = Window.GetWindow(this);
                mainWindow.Hide();
                Core.调用程序exe("ShellToolBox.exe", "来自Shell应用程序调用 LoginLauncher");
                mainWindow.Close();
            }
            else
            {
                bt_login.Background = Brushes.Red;
                txt_login.Text = result;
            }
        }
    }
}
