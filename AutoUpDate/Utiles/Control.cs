using System.Diagnostics;

namespace AutoUpDate.Utiles
{
    public class Control
    {
        //button_Cancel按钮的可视化控制
        public static void 关闭进程(string p_name)
        {
            //关闭进程ShellToolBox.exe
            Process[] processes = Process.GetProcessesByName(p_name);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
