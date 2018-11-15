using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFW.WF
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new TestWindow());
             return;
            //Application.Run(new CallWindow(new LALLib.Call(11)));
            //return;

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult == DialogResult.OK)
            {
                Application.Run(new MainWindow());
            }
            else
            {
                return;
            }
        }
    }
}
