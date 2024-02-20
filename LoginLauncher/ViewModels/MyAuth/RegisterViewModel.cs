using System.ComponentModel;

namespace LoginLauncher.ViewModels.MyAuth
{
    public class RegistrationData
    {
        public RegistrData Data { get; set; }
        public string? skey { get; set; }
        public string? vkey { get; set; }
        public string? sign { get; set; }
    }

    public partial class RegistrData : INotifyPropertyChanged
    {
        public string? device_info { get; set; }
        public string? device_code { get; set; }

        //private string? user;
        //public string? User { get => user; set { user = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(User))); } }

        //private string? pass;
        //public string? Pass { get => pass; set { pass = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pass))); } }
        private string? User;
        public string? user { get => User; set { User = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(user))); } }
        private string? Pass;
        public string? pass { get => Pass; set { Pass = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(pass))); } }

        //[ObservableProperty]
        //private string? user;
        //[ObservableProperty]
        //private string? pass;

        //public string? ckey { get; set; }
        public string? name
        { get; set; }
        public string? qq { get; set; }
        public string? remark { get; set; }
        public string? timestamp { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
