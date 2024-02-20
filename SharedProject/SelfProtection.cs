using System.Diagnostics;

public class SelfProtection
{
    public static void 调试模式关闭进程()
    {
        //判断是否是调试模式
        if (StaticGlobalVar.IsDebugMode)
        {
            //结束进程
            Process.GetCurrentProcess().Kill();
        }
    }
}