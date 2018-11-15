using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFW.WF
{
    public partial class TestWindow : Form
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            msgPnl.ShowMessage(true, this,"ssss", UX.MessageBoxButtonType.Answer, MessageBoxIcon.Information, () =>
             {
                 MessageBox.Show("fdaf");
             });
        }
    }
}
