using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFW.LALLib;

namespace MFW.WF.UX
{
    public partial class ContentSelectPanel : UserControl
    {
        public ContentSelectPanel()
        {
            InitializeComponent();
            cbxFormat.SelectedIndex = 0;
        }

        public IList<Device> Monitors
        {
            set
            {
                cbxMonitor.DataSource = value;
                cbxMonitor.SelectedIndex = 0;
            }
        }
        public IList<Device> Apps
        {
            set
            {
                cbxApp.DataSource = value;
                cbxApp.SelectedIndex = 0;
            }
        }

        private void rdoBFCP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBFCP.Checked)
            {
                cbxFormat.Enabled = true;
            }
            else
            {
                cbxMonitor.Enabled = false;
                cbxApp.Enabled = false;
            }
        }

        private void rdMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMonitor.Checked)
            {
                cbxMonitor.Enabled = true;
                cbxApp.Enabled = true;
            }
            else
            {
                cbxFormat.Enabled = true;
            }
        }

        public Func<string,string,string,string,bool> OKAction { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(null != OKAction)
            {
                var type = rdMonitor.Checked ? "Monitor" : "BFCP";
                var result = OKAction(type,cbxFormat.SelectedValue.ToString(),cbxMonitor.SelectedValue.ToString(),cbxApp.SelectedValue.ToString());
                if(result)
                {
                    this.Dispose();
                }
            }
            this.Dispose();
        }

        public Action OnCancel { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke();
            this.Dispose();
        }
    }
}
