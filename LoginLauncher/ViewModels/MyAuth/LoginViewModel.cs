using System;
using System.ComponentModel;

namespace LoginLauncher.ViewModels.MyAuth
{
    public class LoginationData
    {
        public LoginData Data { get; set; }
        public string? skey { get; set; }
        public string? vkey { get; set; }
        public string? sign { get; set; }
    }

    public partial class LoginData : INotifyPropertyChanged
    {
        public string? device_info { get; set; }
        public string? device_code { get; set; }
        private string? User;
        public string? user { get => User; set { User = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(user))); } }
        private string? Pass;
        public string? pass { get => Pass; set { Pass = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(pass))); } }
        public string? ckey { get; set; }
        public string? timestamp { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
