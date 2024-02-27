using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LoginLauncher.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        //[ObservableProperty]
        private string usernameQQ = "66666666";
        public string UsernameQQ
        {
            get => usernameQQ;
            set
            {
                usernameQQ = value;
                SetProperty(ref usernameQQ, value);
                OnPropertyChanged(nameof(UsernameQQ));
            }
        }

        private string userQQ;
        public string UserQQ
        {
            get => userQQ;
            set
            {
                UsernameQQ = "http://q2.qlogo.cn/headimg_dl?dst_uin=" + value + "&spec=100";
                userQQ = value;
                SetProperty(ref userQQ, value);
            }
        }

    }

    public partial class MWViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private MainWindowViewModel viewdata = new MainWindowViewModel();
    }
}
