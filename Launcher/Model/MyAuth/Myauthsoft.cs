using System;
using System.Diagnostics;
using LoginLauncher.ViewModels.MyAuth;
using RestSharp;

namespace LoginLauncher.Model.MyAuth
{
    public class Myauthsoft     //软件接口
    {
        public static string CheckUpdate()     //检查更新
        {
            string endpoint = "/myauth/soft/checkUpdate";
            var requestData = new CheckUpdate
            {
                Data = new CheckUpdateData
                {
                    device_info = DataSource.Device_info,
                    device_code = DataSource.Device_code,
                    timestamp = DataSource.Timestamp
                },
                skey = DataSource.Skey,
                vkey = DataSource.Vkey
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
                Debug.WriteLine("/myauth/soft/checkUpdate  Exception: " + ex.Message);
                Debug.WriteLine("/myauth/soft/checkUpdate  Stack Trace: " + ex.StackTrace);
            }


            // 发送POST请求
            var client = new RestClient(Api.apiUrl);
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(requestData);

            RestResponse response = client.Execute<RestResponse>(request);
            Debug.WriteLine("/myauth/soft/checkUpdate: " + response.Content);

            return ShellTool.解析json(response).msg;
        }


    }
}
