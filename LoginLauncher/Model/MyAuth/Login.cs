using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using LoginLauncher.ViewModels.MyAuth;
using LoginLauncher.ViewModels.MyAuth.Soft;
using RestSharp;

namespace LoginLauncher.Model.MyAuth
{
    public class Login
    {
        public string SoftLogin()
        {
            string endpoint = "/myauth/soft/login";

            var requestData = new LoginationData
            {
                Data = new LoginData
                {
                    device_info = DataSource.Device_info,
                    device_code = DataSource.Device_code,
                    user = DataSource.MWVM.Viewdata.UserQQ,
                    pass = DataSource.loginView.login_pass.Password,
                    ckey = "",
                    timestamp = DataSource.Timestamp
                },
                skey = DataSource.Skey,
                vkey = DataSource.Vkey
                //签名在后面计算出来后添加
            };

            // 计算签名
            try
            {
                // 在此处执行 CalculateSignature 方法
                string sign = Api.CalculateSignature(requestData);

                // 设置签名属性
                requestData.sign = sign;

            }
            catch (Exception ex)
            {
                // 捕获异常并记录堆栈跟踪信息
                Debug.WriteLine("Exception: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
            }

            return StartPost(endpoint, requestData);
            // 发送POST请求
            //var client = new RestClient(Api.apiUrl);
            //var request = new RestRequest(endpoint, Method.Post);
            //request.AddJsonBody(requestData);

            //RestResponse response = client.Execute<RestResponse>(request);
            //Debug.WriteLine("登录Response: " + response.Content);
            //return ShellTool.解析json(response).msg;
        }
        public void CreateHeart1()
        {
            // 创建一个异步方法
            async void DoHeartAsync()
            {
                while (true)
                {
                    // 执行心跳逻辑
                    string msg = Heart();
                    Debug.WriteLine(msg);

                    // 等待 100 毫秒
                    await Task.Delay(100);
                }
            }

            // 启动异步方法
            DoHeartAsync();
        }

        public void CreateHeart()
        {


            // 创建一个 DispatcherTimer 实例
            DispatcherTimer timer = new DispatcherTimer();

            // 使用 WeakReference 来防止 DispatcherTimer 被垃圾回收
            WeakReference<DispatcherTimer> timerWeakReference = new WeakReference<DispatcherTimer>(null);

            // 将 DispatcherTimer 实例添加到 WeakReference 中
            timerWeakReference.SetTarget(timer);
            // 设置时间间隔
            timer.Interval = TimeSpan.FromMilliseconds(10000);

            // 添加 Tick 事件处理程序
            timer.Tick += Timer_Tick;

            // 启动 DispatcherTimer
            timer.Start();

            // Tick 事件处理程序
            void Timer_Tick(object sender, EventArgs e)
            {
                string msg = Heart();
                Debug.WriteLine(msg);
            }

        }

        public string Heart()
        {
            string endpoint = "/myauth/soft/heart";

            var requestData = new LoginationData
            {
                Data = new LoginData
                {
                    device_info = DataSource.Device_info,
                    device_code = DataSource.Device_code,
                    token = DataSource.Token,
                    timestamp = DataSource.Timestamp
                },
                skey = DataSource.Skey,
                vkey = DataSource.Vkey
                //签名在后面计算出来后添加
            };

            // 计算签名
            try
            {
                // 在此处执行 CalculateSignature 方法
                string sign = Api.CalculateSignature(requestData);

                // 设置签名属性
                requestData.sign = sign;

            }
            catch (Exception ex)
            {
                // 捕获异常并记录堆栈跟踪信息
                Debug.WriteLine("Exception: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);
            }

            return StartPost(endpoint, requestData);
        }

        private static string StartPost(string endpoint, LoginationData requestData)
        {
            // 发送POST请求
            var client = new RestClient(Api.apiUrl);
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(requestData);
            RestResponse response = client.Execute<RestResponse>(request);
            Debug.WriteLine(endpoint + "Response: " + response.Content);
            //登录的时候把token取出来
            if (endpoint == "/myauth/soft/login")
            {
                // 匹配 token 字符串
                Match match = Regex.Match(response.Content, @"token"":""(?<token>.+?)""");

                // 从匹配结果中提取 token 值
                string token = match.Groups["token"].Value;
                DataSource.Token = token;
            }
            return ShellTool.解析json(response).msg;
        }
    }
}
