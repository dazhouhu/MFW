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
        private UXMessageMask()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public static void ShowMessage(Form ownerForm,bool isModal,string msg, MessageBoxButtonsType btnType, MessageBoxIcon boxIcon
            , Action okAction = null, Action cancelAction = null, Action noAction = null)
        {
            HideMessage(ownerForm);

            var msgPnl = new UXMessageMask() {
                Name = "msgPnl"
            };
            ownerForm.Controls.Add(msgPnl);

            msgPnl.BackColor = Color.FromArgb(127, 255, 255,255);
            msgPnl.Left = 0;
            msgPnl.Top = 0;
            msgPnl.Width = ownerForm.Width;
            msgPnl.Height = ownerForm.Height;
            msgPnl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            msgPnl.BringToFront();

            if (isModal)
            {
                var win = new UXMessageWindow()
                {
                    Message = msg,
                    MessageBoxButtonsType = btnType,
                    MessageBoxIcon = boxIcon,
                    OKAction = okAction,
                    CancelAction = cancelAction,
                    NoAction = noAction,
                    Owner = ownerForm
                };
                win.FormClosed += (sender, args) => { HideMessage(ownerForm); };
                win.ShowDialog();                
            }
            else
            {
                var msgBox = new UXMessagePanel()
                {
                    Message = msg,
                    MessageBoxButtonsType = btnType,
                    MessageBoxIcon = boxIcon,
                    OKAction = okAction,
                    CancelAction = cancelAction,
                    NoAction = noAction
                };
                msgPnl.Controls.Add(msgBox);
                var x = (ownerForm.Width - msgBox.Width) / 2;
                var y = (ownerForm.Height - msgBox.Height) / 2;
                msgBox.Location = new Point(x, y);
                msgBox.Disposed += (obj, args) => { HideMessage(ownerForm); };
            }
        }

        public static void HideMessage(Form ownerForm)
        {
            if (null == ownerForm)
            {
                return;
            }
            if (ownerForm.Controls.ContainsKey("msgPnl"))
            {
                ownerForm.Controls.RemoveByKey("msgPnl");
            }
        }

        public static void ShowForm(Form ownerForm, Control pnl)
        {
            HideMessage(ownerForm);

            var msgPnl = new UXMessageMask()
            {
                Name = "msgPnl"
            };
            ownerForm.Controls.Add(msgPnl);
            msgPnl.BringToFront();
            
            msgPnl.Controls.Add(pnl);
            var x = (ownerForm.Width - pnl.Width) / 2;
            var y = (ownerForm.Height - pnl.Height) / 2;
            pnl.Location = new Point(x, y);
            pnl.Disposed += (obj, args) => {
                HideMessage(ownerForm);
            };
        }
    }
}