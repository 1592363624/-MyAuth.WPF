using Newtonsoft.Json;

namespace LoginLauncher.ViewModels.MyAuth
{
    public class ApiResponse
    {
        public string? code { get; set; }
        public string? success { get; set; }
        public string? msg { get; set; }
        public string? json { get; set; }
        public string? ip { get; set; }
        public string? token { get; set; }
        public Ban? Ban { get; set; }

    }

    public class Ban
    {
        public string? type { get; set; }
        public string? value { get; set; }
        public string? toTime { get; set; }
        public string? addTime { get; set; }
        public string? why { get; set; }

    }
}
