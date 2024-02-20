using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutoUpDate.Model;
using AutoUpDate.ViewModel;
using Downloader;
using Newtonsoft.Json;

namespace AutoUpDate.Utiles
{
    public static class StarDownload
    {
        //private const string DownloadListFile = "DownloadList.json";
        private static List<DownloadItem> DownloadList;
        private static DownloadService CurrentDownloadService;
        private static DownloadConfiguration CurrentDownloadConfiguration;
        private static CancellationTokenSource CancelAllTokenSource;
        public static string 总下载量;
        public static string 当前下载量;


        public async static Task StarDown()
        {
            //Initial();
            await DownloadAll(DownloadList, CancelAllTokenSource.Token).ConfigureAwait(false);
        }

        private static async Task DownloadAll(IEnumerable<DownloadItem> downloadList, CancellationToken cancelToken)
        {
            foreach (DownloadItem downloadItem in downloadList)
            {
                if (cancelToken.IsCancellationRequested)
                    return;

                // begin download from url
                await DownloadFile(downloadItem).ConfigureAwait(false);
            }
        }
        public static void Initial()
        {
            CancelAllTokenSource = new CancellationTokenSource();
            DownloadList = GetDownloadItems();
        }

        /// <summary>
        /// 获取下载文件列表
        /// </summary>
        /// <returns>下载列表</returns>
        private static List<DownloadItem> GetDownloadItems()
        {
            //List<DownloadItem>? downloadList = File.Exists(DownloadListFile)
            //    ? JsonConvert.DeserializeObject<List<DownloadItem>> (GetDownloadListFileAsync())
            //    : null;


            List<DownloadItem>? downloadList = JsonConvert.DeserializeObject<List<DownloadItem>>(GetDownloadListFileAsync());

            if (downloadList == null)
            {
                MessageBox.Show("下载列表文件不存在,程序可能异常结束!!!");
            }

            foreach (DownloadItem dl in downloadList)
            {
                StaticDataSource.downloadItem.Updatalog = dl.Updatalog;
                StaticDataSource.downloadItem.Version = dl.Version;
                StaticDataSource.downloadItem.FileName = FileUtil.获取桌面路径() + @"\" + dl.FileName;
                Debug.WriteLine(StaticDataSource.downloadItem.FileName);
                //替换downloadList里面的FileName
                downloadList = downloadList.Select(x => { x.FileName = StaticDataSource.downloadItem.FileName; return x; }).ToList();
            }

            return downloadList;
        }



        private static string GetDownloadListFileAsync()
        {
            HttpClient httpClient = new HttpClient();
            string url = "https://gitee.com/Shell520/shell/raw/master/admin/ShellToolBox";
            var response = httpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return result;

        }

        private static async Task<DownloadService> DownloadFile(DownloadItem downloadItem)
        {
            CurrentDownloadConfiguration = GetDownloadConfiguration();
            CurrentDownloadService = CreateDownloadService(CurrentDownloadConfiguration);

            if (string.IsNullOrWhiteSpace(downloadItem.FileName))
            {
                await CurrentDownloadService.DownloadFileTaskAsync(downloadItem.Url, new DirectoryInfo(downloadItem.FolderPath)).ConfigureAwait(false);
            }
            else
            {
                await CurrentDownloadService.DownloadFileTaskAsync(downloadItem.Url, downloadItem.FileName).ConfigureAwait(false);
            }

            return CurrentDownloadService;
        }

        private static DownloadConfiguration GetDownloadConfiguration()
        {
            var cookies = new CookieContainer();
            cookies.Add(new Cookie("download-type", "test") { Domain = "domain.com" });

            return new DownloadConfiguration
            {
                BufferBlockSize = 10240,    // usually, hosts support max to 8000 bytes, default values is 8000
                ChunkCount = 8,             // file parts to download, default value is 1
                MaximumBytesPerSecond = 1024 * 1024 * 10, // download speed limited to 10MB/s, default values is zero or unlimited
                MaxTryAgainOnFailover = 5,  // the maximum number of times to fail
                MaximumMemoryBufferBytes = 1024 * 1024 * 50, // release memory buffer after each 50 MB
                ParallelDownload = true,    // download parts of file as parallel or not. Default value is false
                ParallelCount = 4,          // number of parallel downloads. The default value is the same as the chunk count
                Timeout = 3000,             // timeout (millisecond) per stream block reader, default value is 1000
                RangeDownload = false,      // set true if you want to download just a specific range of bytes of a large file
                RangeLow = 0,               // floor offset of download range of a large file
                RangeHigh = 0,              // ceiling offset of download range of a large file
                ClearPackageOnCompletionWithFailure = true, // Clear package and downloaded data when download completed with failure, default value is false
                MinimumSizeOfChunking = 1024, // minimum size of chunking to download a file in multiple parts, default value is 512                                              
                ReserveStorageSpaceBeforeStartingDownload = true, // Before starting the download, reserve the storage space of the file as file size, default value is false
                RequestConfiguration =
                {
                    // config and customize request headers
                    Accept = "*/*",
                    CookieContainer = cookies,
                    Headers = new WebHeaderCollection(),     // { your custom headers }
                    KeepAlive = true,                        // default value is false
                    ProtocolVersion = HttpVersion.Version11, // default value is HTTP 1.1
                    UseDefaultCredentials = false,
                    // your custom user agent or your_app_name/app_version.
                    UserAgent = $"DownloaderSample/{Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}"
                    // Proxy = new WebProxy() {
                    //    Address = new Uri("http://YourProxyServer/proxy.pac"),
                    //    UseDefaultCredentials = false,
                    //    Credentials = System.Net.CredentialCache.DefaultNetworkCredentials,
                    //    BypassProxyOnLocal = true
                    // }
                }
            };
        }

        private static DownloadService CreateDownloadService(DownloadConfiguration config)
        {

            StaticDataSource.downloadService = new DownloadService(config);

            // Provide `FileName` and `TotalBytesToReceive` at the start of each downloads
            StaticDataSource.downloadService.DownloadStarted += OnDownloadStarted;

            // Provide any information about chunker downloads, 
            // like progress percentage per chunk, speed, 
            // total received bytes and received bytes array to live streaming.
            //downloadService.ChunkDownloadProgressChanged += OnChunkDownloadProgressChanged;

            // Provide any information about download progress, 
            // like progress percentage of sum of chunks, total speed, 
            // average speed, total received bytes and received bytes array 
            // to live streaming.
            StaticDataSource.downloadService.DownloadProgressChanged += OnDownloadProgressChanged;

            // Download completed event that can include occurred errors or 
            // cancelled or download completed successfully.
            StaticDataSource.downloadService.DownloadFileCompleted += OnDownloadFileCompleted;

            return StaticDataSource.downloadService;
        }

        private static void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"下载失败: {e.Error.Message}");
            }
            else
            {
                StaticDataSource.downloadItem.下载速度 = "下载完成";
            }
        }

        //private static void OnChunkDownloadProgressChanged(object? sender, Downloader.DownloadProgressChangedEventArgs e)
        //{
        //  Debug.WriteLine($"下载进度 {e.ProgressPercentage:P} 速度 {e.BytesPerSecondSpeed.CalcMemoryMensurableUnit()}/s");
        //}

        private static void OnDownloadStarted(object sender, DownloadStartedEventArgs e)
        {
            Debug.WriteLine($"正在下载文件: {e.FileName} 大小 {e.TotalBytesToReceive} bytes.");
            总下载量 = $"{e.TotalBytesToReceive.CalcMemoryMensurableUnit()}";
        }

        private static void OnDownloadProgressChanged(object sender, Downloader.DownloadProgressChangedEventArgs e)
        {
            //DataSource dataSource = new DataSource();
            //dataSource.ProgressPercentage = (double)e.ProgressPercentage;
            //Debug.WriteLine($"下载进度 {dataSource.ProgressPercentage} 速度 {e.BytesPerSecondSpeed.CalcMemoryMensurableUnit()}/s");
            当前下载量 = $"{e.ReceivedBytesSize.CalcMemoryMensurableUnit()}";
            StaticDataSource.downloadItem.下载进度 = 当前下载量 + " / " + 总下载量;
            StaticDataSource.downloadItem.下载速度 = $"{e.BytesPerSecondSpeed.CalcMemoryMensurableUnit()}/s";
            StaticDataSource.downloadItem.ProgressPercentage = (double)e.ProgressPercentage;
        }
    }
}
