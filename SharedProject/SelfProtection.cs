using System.Diagnostics;

public class SelfProtection
{
    public static void ����ģʽ�رս���()
    {
        //�ж��Ƿ��ǵ���ģʽ
        if (StaticGlobalVar.IsDebugMode)
        {
            //��������
            Process.GetCurrentProcess().Kill();
        }
    }
}