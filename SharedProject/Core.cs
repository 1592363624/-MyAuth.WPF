using System.Diagnostics;
using System.Windows;
using System;

public class Core
{
    /// <summary>
    /// parameter���� ��һ��:Ҫ�����ĳ�������,�ڶ���:�����������(���ڹرձ�����) ����ֵ:0�����˳�,-1�쳣�˳�
    /// </summary>
    /// <param name="exe_name">Ҫ�����ĳ�������</param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static int ���ó���exe(string exe_name, string parameter)
    {
        //Visibility = Visibility.Hidden;
        //ȡ��Ŀ¼��exe·��
        string? path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        // Ҫ���õĳ���·��
        string programPath = path + exe_name;
        // Ҫ���ݵĲ���
        string argument = parameter;

        try
        {
            Process pro = new Process();
            pro.StartInfo.FileName = programPath;
            pro.StartInfo.Arguments = argument;
            pro.Start();
            // �ȴ����̽���
            pro.WaitForExit();
            //Visibility = Visibility.Visible;
            // ������̵��˳�����
            Debug.WriteLine("Exit code: {0}", pro.ExitCode);
            if (pro.ExitCode != 0)
            {
                //MessageBox.Show("�����쳣�˳�");
                return 0;
            }
            return 0;

        }
        catch (Exception ex)
        {
            MessageBox.Show("Error��" + ex.Message);
            return -1;
        }
    }
}

