using System.Diagnostics;
using System.Windows;
using System;

public class Core
{
    /// <summary>
    /// parameter参数 第一个:要启动的程序名字,第二个:本程序的名字(用于关闭本程序) 返回值:0正常退出,-1异常退出
    /// </summary>
    /// <param name="exe_name">要启动的程序名字</param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static int 调用程序exe(string exe_name, string parameter)
    {
        //Visibility = Visibility.Hidden;
        //取根目录下exe路径
        string? path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        // 要调用的程序路径
        string programPath = path + exe_name;
        // 要传递的参数
        string argument = parameter;

        try
        {
            Process pro = new Process();
            pro.StartInfo.FileName = programPath;
            pro.StartInfo.Arguments = argument;
            pro.Start();
            // 等待进程结束
            pro.WaitForExit();
            //Visibility = Visibility.Visible;
            // 输出进程的退出代码
            Debug.WriteLine("Exit code: {0}", pro.ExitCode);
            if (pro.ExitCode != 0)
            {
                //MessageBox.Show("程序异常退出");
                return 0;
            }
            return 0;

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error：" + ex.Message);
            return -1;
        }
    }
}

