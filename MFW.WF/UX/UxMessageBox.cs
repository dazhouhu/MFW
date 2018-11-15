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
    public partial class UxMessageBox : Form
    {
        #region Constructors
        public UxMessageBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private CustomMessageBoxButton _btns = CustomMessageBoxButton.OK;
        public CustomMessageBoxButton Btns
        {
            get { return _btns; }
            set { _btns = value; }
        }
        private CustomMessageBoxIcon _messageIcon = CustomMessageBoxIcon.None;
        public CustomMessageBoxIcon MessageIcon
        {
            get { return _messageIcon; }
            set { _messageIcon = value; }
        }

        public string Message { get; set; }

        private string _okButtonText = "确定  ";
        public string OKButonText
        {
            get { return _okButtonText; }
            set { _okButtonText = value; }
        }

        private string _yesButtonText = "是  ";
        public string YesButonText
        {
            get { return _yesButtonText; }
            set { _yesButtonText = value; }
        }

        private string _noButtonText = "否  ";
        public string NoButonText
        {
            get { return _noButtonText; }
            set { _noButtonText = value; }
        }

        private string _cancelButtonText = "取消  ";
        public string CancelButonText
        {
            get { return _cancelButtonText; }
            set { _cancelButtonText = value; }
        }
        #endregion


        #region Actions
        public Action OKAction { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            OKAction?.Invoke();
            this.Close();
        }

        public Action CancelAction { get; set; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelAction?.Invoke();
            this.Close();
        }
        #endregion

        public void ShowMessage()
        {
            this.Visibility = Visibility.Visible;
            #region CustomMessageBoxButton
            switch (Btns)
            {
                case CustomMessageBoxButton.OK:
                    {
                        btnOk.Content = OKButonText;
                        btnOk.Icon = new BitmapImage(new Uri(@"/Assets/Icons/ok.png", UriKind.Relative));
                        btnOk.IsDefault = true;
                        btnOk.Background = Brushes.CornflowerBlue;
                        btnOk.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Collapsed;
                        btnNo.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomMessageBoxButton.OKCancel:
                    {
                        btnOk.Content = OKButonText;
                        btnOk.Icon = new BitmapImage(new Uri(@"/Assets/Icons/ok.png", UriKind.Relative));
                        btnOk.Visibility = Visibility.Visible;
                        btnOk.Background = Brushes.CornflowerBlue;
                        btnOk.IsDefault = true;
                        btnCancel.Content = CancelButonText;
                        btnCancel.Background = Brushes.LightGray;
                        btnCancel.Icon = new BitmapImage(new Uri(@"/Assets/Icons/cancel.png", UriKind.Relative));
                        btnCancel.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Collapsed;
                        btnNo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomMessageBoxButton.YesNo:
                    {
                        btnOk.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                        btnYes.Visibility = Visibility.Visible;
                        btnYes.Content = YesButonText;
                        btnYes.Background = Brushes.CornflowerBlue;
                        btnYes.Icon = new BitmapImage(new Uri(@"/Assets/Icons/ok.png", UriKind.Relative));
                        btnYes.IsDefault = true;
                        btnNo.Content = NoButonText;
                        btnNo.Background = Brushes.LightGray;
                        btnNo.Icon = new BitmapImage(new Uri(@"/Assets/Icons/cancel.png", UriKind.Relative));
                        btnNo.Visibility = Visibility.Visible;
                    }
                    break;
                case CustomMessageBoxButton.YesNoCancel:
                    {
                        btnOk.Visibility = Visibility.Collapsed;
                        btnCancel.Content = CancelButonText;
                        btnCancel.Background = Brushes.LightGray;
                        btnCancel.Icon = new BitmapImage(new Uri(@"/Assets/Icons/cancel.png", UriKind.Relative));
                        btnCancel.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Visible;
                        btnYes.Content = YesButonText;
                        btnYes.IsDefault = true;
                        btnYes.Background = Brushes.CornflowerBlue;
                        btnYes.Icon = new BitmapImage(new Uri(@"/Assets/Icons/ok.png", UriKind.Relative));
                        btnNo.Content = NoButonText;
                        btnNo.Background = Brushes.DarkTurquoise;
                        btnNo.Icon = new BitmapImage(new Uri(@"/Assets/Icons/cancel.png", UriKind.Relative));
                        btnNo.Visibility = Visibility.Visible;
                    }
                    break;
                case CustomMessageBoxButton.Answer:
                    {
                        btnOk.Content = "接听";
                        btnOk.Icon = new BitmapImage(new Uri(@"/Assets/Icons/answer.png", UriKind.Relative));
                        btnOk.IsDefault = true;
                        btnOk.Background = Brushes.CornflowerBlue;
                        btnOk.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Collapsed;
                        btnNo.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomMessageBoxButton.Hangup:
                    {
                        btnOk.Content = "挂断";
                        btnOk.Icon = new BitmapImage(new Uri(@"/Assets/Icons/hangup.png", UriKind.Relative));
                        btnOk.IsDefault = true;
                        btnOk.Background = Brushes.CornflowerBlue;
                        btnOk.Foreground = Brushes.Red;
                        btnOk.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Collapsed;
                        btnNo.Visibility = Visibility.Collapsed;
                        btnCancel.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomMessageBoxButton.AnswerHangup:
                    {
                        btnOk.Content = "接听";
                        btnOk.Icon = new BitmapImage(new Uri(@"/Assets/Icons/answer.png", UriKind.Relative));
                        btnOk.IsDefault = true;
                        btnOk.Background = Brushes.CornflowerBlue;
                        btnOk.Foreground = Brushes.Black;
                        btnOk.Visibility = Visibility.Visible;
                        btnCancel.Content = "挂断";
                        btnCancel.Background = Brushes.LightGray;
                        btnCancel.Foreground = Brushes.Red;
                        btnCancel.Icon = new BitmapImage(new Uri(@"/Assets/Icons/hangup.png", UriKind.Relative));
                        btnCancel.Visibility = Visibility.Visible;
                        btnYes.Visibility = Visibility.Collapsed;
                        btnNo.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
            #endregion
            #region CustomMessageBoxIcon
            switch (MessageIcon)
            {
                case CustomMessageBoxIcon.None:
                    {
                        msgIcon.Visibility = Visibility.Collapsed;
                    }
                    break;
                case CustomMessageBoxIcon.Error:
                    {
                        msgIcon.Visibility = Visibility.Visible;
                        msgIcon.Source = new BitmapImage(new Uri(@"/Assets/Icons/error.png", UriKind.Relative));
                    }
                    break;
                case CustomMessageBoxIcon.Info:
                    {
                        msgIcon.Visibility = Visibility.Visible;
                        msgIcon.Source = new BitmapImage(new Uri(@"/Assets/Icons/info.png", UriKind.Relative));
                    }
                    break;
                case CustomMessageBoxIcon.Question:
                    {
                        msgIcon.Visibility = Visibility.Visible;
                        msgIcon.Source = new BitmapImage(new Uri(@"/Assets/Icons/question.png", UriKind.Relative));
                    }
                    break;
                case CustomMessageBoxIcon.Warning:
                    {
                        msgIcon.Visibility = Visibility.Visible;
                        msgIcon.Source = new BitmapImage(new Uri(@"/Assets/Icons/alert.png", UriKind.Relative));
                    }
                    break;
            }
            #endregion
            this.lblMsg.Text = Message;
        }

        public void HideMessage()
        {
            this.Close();
        }
    }

    /// <summary>
    /// 显示按钮类型
    /// </summary>
    public enum CustomMessageBoxButton
    {
        OK = 0,
        OKCancel = 1,
        YesNo = 2,
        YesNoCancel = 3,
        Answer = 4,
        Hangup = 5,
        AnswerHangup = 6
    }
    /// <summary>
    /// 消息框的返回值
    /// </summary>
    /// <summary>
    /// 图标类型
    /// </summary>
    public enum CustomMessageBoxIcon
    {
        None = 0,
        Info,
        Error,
        Question,
        Warning
    }
}
