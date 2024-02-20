using System;
using LoginLauncher.Model.MyAuth;
using LoginLauncher.ViewModels.MyAuth;
using LoginLauncher.Views;

namespace LoginLauncher
{
    public static class DataSource
    {
        public static RegistrData registrData = new RegistrData();
        public static Register register = new Register();
        public static RegisterView registerView = new RegisterView();
        public static Login login = new Login();
        public static LoginData loginData = new LoginData();
        public static LoginView loginView = new LoginView();

        public static string Device_info = "null";
        public static string Device_code = "null";
        public static string ComputerUserName;
        public static string Timestamp = Api.GetCurrentTimestamp();

        public static string Skey = "5403ebb3-282d-48e1-9653-e164ee123375";
        public static string Vkey = "082CE72C-06EE-4861-ABD6-F7D6D0929C21";
        public static string gen_key = "gen_key=1234561592363624";
    }

}
