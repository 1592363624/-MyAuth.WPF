using System;
using LoginLauncher.Model.MyAuth;
using LoginLauncher.ViewModels;
using LoginLauncher.ViewModels.MyAuth;
using LoginLauncher.ViewModels.MyAuth.Soft;
using LoginLauncher.Views;

namespace LoginLauncher
{
    public static class DataSource
    {
        public static RegistrData registrData = new RegistrData();
        public static Register register = new Register();
        public static RegisterView registerView = new RegisterView();
        public static Login login = new Login();
        public static Heart heart = new Heart();
        public static LoginData loginData = new LoginData();
        public static LoginView loginView = new LoginView();
        public static MWViewModel MWVM = new MWViewModel();


        public static string Device_info = "null";
        public static string Device_code = "null";
        public static string? ComputerUserName;
        public static string? Token;
        public static string Timestamp = Api.GetCurrentTimestamp();

        public static string Skey = " ";
        public static string Vkey = " ";
        public static string gen_key = " ";
    }

}
