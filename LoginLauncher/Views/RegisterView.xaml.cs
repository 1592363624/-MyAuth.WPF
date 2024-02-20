using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LoginLauncher.Views
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : Page
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = DataSource.registrData;
            Loaded += RegisterView_Loaded;
        }


        private void Regbutton_Click(object sender, RoutedEventArgs e)
        {
            string result = DataSource.register.SoftRegister();
            if (result == "注册成功")
            {
                this.NavigationService.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));
            }
            else
            {
                bt_reg.Background = Brushes.Red;
                txt_reg.Text = result;
            }
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
    }
}
