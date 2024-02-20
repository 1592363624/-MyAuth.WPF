using LoginLauncher.ViewModels.MyAuth;
using Newtonsoft.Json;
using RestSharp;

namespace LoginLauncher.Model
{
    public static class ShellTool
    {
        public static ApiResponse 解析json(RestResponse response)
        {

            // 假设 response 是 RestResponse 对象
            string jsonString = response.Content; // 获取 JSON 响应字符串

            // 使用 Newtonsoft.Json 库来解析 JSON
            var refjson = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

            // 提取 msg 字段的值
            return refjson;
        }
    }


}
