using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginLauncher.Views
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : Page
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
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
