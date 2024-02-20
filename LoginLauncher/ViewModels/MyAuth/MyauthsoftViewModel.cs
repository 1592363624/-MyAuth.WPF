namespace LoginLauncher.ViewModels.MyAuth
{
    public partial class CheckUpdate
    {
        public required CheckUpdateData Data { get; set; }
        public string? skey { get; set; }
        public string? vkey { get; set; }
        public string? sign { get; set; }
    }
    public partial class CheckUpdateData
    {
        public string? device_info { get; set; }
        public string? device_code { get; set; }
        public string? timestamp { get; set; }
    }
}
