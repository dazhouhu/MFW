using log4net;
using MFW.LALLib;
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
    public partial class LoginWindow : Form
    {
        #region Fields
        private ILog log = LogManager.GetLogger("MFWLogin");
        private EventMonitor eventMonitor = EventMonitor.GetInstance();
        private LALProperties lalProperties = LALProperties.GetInstance();
        #endregion

        public LoginWindow()
        {
            InitializeComponent();
        }
        #region CallBacks       
        private void monitorEventHandle(Event evt)
        {
            switch (evt.EventType)
            {
                case EventTypeEnum.SIP_REGISTER_SUCCESS:  /* with SIP server from CC*/
                    {
                        this.DialogResult = DialogResult.OK;    //返回一个登录成功的对话框状态
                        this.Close();                           //关闭登录窗口
                    }
                    break;
                case EventTypeEnum.SIP_REGISTER_FAILURE:  /* from CC */
                    {
                        MessageBox.Show(this, "登录失败!", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLogin.Enabled = true;
                    }
                    break;
                case EventTypeEnum.PLCM_MFW_SIP_REGISTER_UNREGISTERED:
                    {
                        btnLogin.Enabled = true;
                    }
                    break;
            }
        }
        #endregion

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            eventMonitor.MonitorEvent += monitorEventHandle;
            eventMonitor.Dispatcher = this;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region Valid
            var sipServer =  txtServer.Text.Trim();
            if (string.IsNullOrEmpty(sipServer))
            {
                txtServer.Focus();
                MessageBox.Show(this,"Sip服务地址必须", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var userName = txtUserName.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                txtUserName.Focus();
                MessageBox.Show(this,"用户名必须", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var pwd = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                txtPassword.Focus();
                MessageBox.Show(this,"密码必须", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            if (!LAL.Initialize())
            {
                MessageBox.Show(this,"初始化失败!", "消息通知",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            if (!LAL.RegisterClient(sipServer, userName, pwd))
            {
                MessageBox.Show(this, "登录失败!", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnLogin.Enabled = false;
            }
        }

    }
}
