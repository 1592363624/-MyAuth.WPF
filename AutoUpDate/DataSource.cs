using AutoUpDate.ViewModel;
using Downloader;

namespace AutoUpDate
{
    //public partial class DataSource : ObservableObject
    //{
    //    // 定义 Updatalog 变量
    //    [ObservableProperty]
    //    private static string updatalog;

    //    //[ObservableProperty]
    //    //private static double progressPercentage;

    //}

    public static class StaticDataSource
    {
        public static DownloadItem downloadItem = new DownloadItem();

        public static DownloadService? downloadService;

        public static string 上级调用程序名 = "";
        public static int 接收到的参数个数 = 0;

    }

    //public static class StaticDataSource
    //{
    //    // 定义 Updatalog 变量

    //    public static string ProgressPercentage { get; set; }
    //}
}