using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFW.WF.UX;

namespace MFW.WF
{
    public partial class TestWindow : Form
    {
        public TestWindow()
        {
            InitializeComponent();
            panel3.BackColor = Color.FromArgb(80, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UXMessageMask.ShowMessage(this, false, "fdasfds", MessageBoxButtonsType.AnswerHangup, MessageBoxIcon.Error);
        }
    }
}
