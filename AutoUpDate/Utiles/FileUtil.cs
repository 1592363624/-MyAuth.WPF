using System;
using System.IO;

namespace AutoUpDate.Utiles
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 获取当前运行文件完整路径
        /// </summary>
        public static string 当前运行文件完整路径 { get; private set; }

        /// <summary>
        /// 获取当前文件目录，不加文件名及后缀
        /// </summary>
        public static string 当前文件目录 { get; private set; }

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        static FileUtil()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
            当前运行文件完整路径 = Environment.ProcessPath;
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
            当前文件目录 = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        public static void FileReName(string oldPath, string newPath)
        {
            var ReName = new FileInfo(oldPath);
            ReName.MoveTo(newPath);
        }

        /// <summary>
        /// 给文件名，得出当前目录完整路径，AppName带文件名后缀
        /// </summary>
        public static string GetCurrFullPath(string appName)
        {
            return Path.Combine(当前文件目录, appName);
        }

        public static string 获取桌面路径()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

    }
}
