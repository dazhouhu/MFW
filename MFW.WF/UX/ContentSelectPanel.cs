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
                if (value.Count > 0)
                {
                    cbxMonitor.SelectedIndex = 0;
                }
            }
        }
        public IList<Device> Apps
        {
            set
            {
                cbxApp.DataSource = value;
                if (value.Count > 0)
                {
                    cbxApp.SelectedIndex = 0;
                }
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
                cbxFormat.Enabled = false;
            }
        }

        public Func<string, BFCPFormatEnum, string,IntPtr,bool> OKAction { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if(null != OKAction)
            {
                var type = rdMonitor.Checked ? "Monitor" : "BFCP";
                var format = BFCPFormatEnum.PLCM_MFW_IMAGE_FORMAT_YV12;
                if (cbxFormat.SelectedIndex >= 0)
                {
                    format = (BFCPFormatEnum)cbxFormat.SelectedIndex;
                }
                string monitor = null;
                if(cbxMonitor.SelectedIndex >= 0)
                {
                    monitor = cbxMonitor.SelectedValue.ToString();
                }
                IntPtr app = IntPtr.Zero;
                if(cbxApp.SelectedIndex>=0)
                {
                    app = (IntPtr) cbxApp.SelectedValue;
                }
                var result = OKAction(type,format,monitor, app);
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
