using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace LoginLauncher.Model.MyAuth
{
    public class Api
    {
        public static string apiUrl = " ";

        // 计算签名
        public static string CalculateSignature(object requestData)
        {
            // 将data节点下的key按字母从小到大排序
            var data = requestData.GetType().GetProperty("Data").GetValue(requestData);
            var sortedData = SortProperties(data);

            // 拼接key=value&key=value形式
            var keyValuePairs = new List<string>();
            foreach (var prop in sortedData)
            {
                var key = prop.Name;
                var value = prop.GetValue(data);
                if (value != null)
                {
                    keyValuePairs.Add($"{key}={value}");
                }
                else
                {
                    // 如果value=null，转换为空字符串
                    keyValuePairs.Add($"{key}=");
                }
            }

            // 添加gen_key
            keyValuePairs.Add(DataSource.gen_key);

            // 将键值对连接成字符串
            string concatenatedData = string.Join("&", keyValuePairs);
            Debug.WriteLine("sign-concatenatedData: " + concatenatedData);
            // 计算MD5
            using (var md5 = MD5.Create())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(concatenatedData);
                byte[] hashBytes = md5.ComputeHash(dataBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                Debug.WriteLine("sign-MD5: " + sb.ToString());
                return sb.ToString();
            }
        }
        public static string GetCurrentTimestamp()
        {
            // 获取当前时间
            DateTimeOffset now = DateTimeOffset.Now;

            // 获取Unix时间戳，以秒为单位
            long unixTimestamp = now.ToUnixTimeSeconds();

            return unixTimestamp.ToString();
        }
        public static string GetOperatingSystemVersion()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject obj in collection)
                {
                    string version = obj["Caption"].ToString(); // 获取操作系统版本
                    return version;
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return "Version information not available";
        }

        public static string GetMachineCode()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject obj in collection)
                {
                    string machineCode = obj["SerialNumber"].ToString(); // 获取系统机器码
                    return machineCode;
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            return "Machine code information not available";
        }
        // 对属性按字母顺序排序
        private static List<System.Reflection.PropertyInfo> SortProperties(object obj)
        {
            var properties = obj.GetType().GetProperties();
            Array.Sort(properties, (x, y) => string.Compare(x.Name, y.Name));
            return new List<System.Reflection.PropertyInfo>(properties);
        }
    }


}
