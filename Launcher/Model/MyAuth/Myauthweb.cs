using System;
using System.Diagnostics;
using System.Windows;
using LoginLauncher.ViewModels.MyAuth;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace LoginLauncher.Model.MyAuth
{
    public class Myauthweb
    {

        public static string Connect()     //检查服务状态
        {
            string endpoint = "/myauth/web/connect";

            // 创建RestClient实例
            var client = new RestClient(Api.apiUrl);

            // 创建GET请求
            var request = new RestRequest(endpoint, Method.Get);

            // 执行GET请求并获取响应
            RestResponse response = client.Execute<JObject>(request);
            Debug.WriteLine("连接response: : " + response.Content);
            //检查响应状态
            if (response.IsSuccessStatusCode)
            {
                return ShellTool.解析json(response).msg;
            }
            else
            {
                return "连接服务器失败";
            }
        }
    }

}
