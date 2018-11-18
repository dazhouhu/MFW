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
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Color drawColor = Color.FromArgb(127, this.BackColor);
            //// 定义画笔
            Pen labelBorderPen = new Pen(drawColor, 0);
            SolidBrush labelBackColorBrush = new SolidBrush(drawColor);
            //// 绘制背景色
            pe.Graphics.DrawRectangle(labelBorderPen, 0, 0, Size.Width, Size.Height);
            pe.Graphics.FillRectangle(labelBackColorBrush, 0, 0, Size.Width, Size.Height);
            

            base.OnPaint(pe);
        }

        public static void ShowMessage(Form ownerForm,bool isModal,string msg, MessageBoxButtonsType btnType, MessageBoxIcon boxIcon
            , Action okAction = null, Action cancelAction = null, Action noAction = null)
        {
            HideMessage(ownerForm);

            var msgPnl = new UXMessageMask() {
                Name = "msgPnl"
            };
            msgPnl.Left = 0;
            msgPnl.Top = 0;
            msgPnl.Width = ownerForm.Width;
            msgPnl.Height = ownerForm.Height;
            msgPnl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            ownerForm.Controls.Add(msgPnl);
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
                var x = (ownerForm.Width - msgBox.Width) / 2;
                var y = (ownerForm.Height - msgBox.Height) / 2;
                msgBox.Location = new Point(x, y);
                msgBox.Disposed += (obj, args) => { HideMessage(ownerForm); };
                msgPnl.Controls.Add(msgBox);
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
            msgPnl.Left = 0;
            msgPnl.Top = 0;
            msgPnl.Width = ownerForm.Width;
            msgPnl.Height = ownerForm.Height;
            msgPnl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            ownerForm.Controls.Add(msgPnl);
            msgPnl.BringToFront();
            
            var x = (ownerForm.Width - pnl.Width) / 2;
            var y = (ownerForm.Height - pnl.Height) / 2;
            pnl.Location = new Point(x, y);
            msgPnl.Controls.Add(pnl);
            pnl.Disposed += (obj, args) => {
                HideMessage(ownerForm);
            };
        }
    }
}