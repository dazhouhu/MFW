using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFW.WF.UX
{
    public partial class UXMessageMask : Panel
    {
        private Form win;
        public UXMessageMask()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(65, 255, 255, 255);
            this.Dock = DockStyle.Fill;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            this.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void ShowMessage(bool isModal,Form  owner, string msg, MessageBoxButtonType btnType, MessageBoxIcon boxIcon
            , Action okAction = null, Action cancelAction = null, Action noAction = null)
        {
            if (null != win)
             {
                win.Close();
            }
            win = new UxMessageBox()
            {
                OwnerCtr = this,
                Message = msg,
                MessageBoxButtonType = btnType,
                MessageBoxIcon = boxIcon,
                OKAction = okAction,
                CancelAction = cancelAction,
                NoAction = noAction,
                Owner = owner
            };
            this.BringToFront();
            this.Visible = true;
            if (isModal)
            {
                var result = win.ShowDialog();
            }
            else
            {
                win.Show();
            }
        }

        public void HideMessage()
        {
            if (null != win)
            {
                win.Close();
            }
            this.Visible = false;
        }

        public void ShowForm(bool isModal, Form form)
        {
            this.BringToFront();
            this.Visible = true;
        }
    }
}