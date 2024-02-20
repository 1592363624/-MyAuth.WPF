using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LoginLauncher.Model.MyAuth;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Windows.Controls;

namespace LoginLauncher.Views
{
    /// <summary>
    /// FirstInitView.xaml 的交互逻辑
    /// </summary>
    public partial class FirstInitView : Window
    {
        private string[] loadingText = new string[] { "系统正在初始化 .", "系统正在初始化 ..", "系统正在初始化 ..." };
        private int currentIndex = 0;
        private DispatcherTimer timer;

        public FirstInitView()
        {
            InitializeComponent();
            this.Topmost = true;
            StartTextAnimation();
        }

        private void StartTextAnimation()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500); // 设置定时器的时间间隔，例如500毫秒  
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            txt_1.Text = loadingText[currentIndex]; // 设置当前显示的文本  
            currentIndex++; // 更新索引以准备下一次迭代  
            if (currentIndex >= loadingText.Length) // 如果到达数组的末尾，重置索引并重新开始循环  
            {
                currentIndex = 0;
            }
        }

        public async Task InitDeviceInfo()
        {
            try
            {
                // 获取任务的结果
                Task<string> operatingSystemVersionTask = Task.Run(() => Api.GetOperatingSystemVersion());
                Task<string> machineCodeTask = Task.Run(() => Api.GetMachineCode());

                // 等待任务完成
                await Task.WhenAll(operatingSystemVersionTask, machineCodeTask);

                // 获取任务的结果
                string operatingSystemVersion = operatingSystemVersionTask.GetAwaiter().GetResult();
                string machineCode = machineCodeTask.GetAwaiter().GetResult();

                // 读取JSON文件
                string filePath = "appsettings.json";
                string jsonContent = File.ReadAllText(filePath);

                // 解析JSON内容
                JObject json = JObject.Parse(jsonContent);
                // 修改获取到的机器信息内容
                json["Info"]["Device_info"] = operatingSystemVersion;
                json["Info"]["Device_code"] = machineCode;
                json["Info"]["ComputerUserName"] = DataSource.ComputerUserName;

                // 将修改后的JSON内容写回文件
                File.WriteAllText(filePath, JsonConvert.SerializeObject(json, Formatting.Indented));
                // 设置设备信息
                DataSource.Device_info = operatingSystemVersion;
                DataSource.Device_code = machineCode;

            }
            catch (Exception ex)
            {
                // 处理异常
                Debug.WriteLine(ex.Message);
            }
        }




        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {




            await InitDeviceInfo();

            //MainWindow mainWindow = new MainWindow();
            // 在 InitDeviceInfo() 执行完成后再关闭窗口
            //await CloseWindowAsync();
            Close();
            //mainWindow.Show();

        }

        private async Task CloseWindowAsync()
        {
            Close();
        }
    }
}
