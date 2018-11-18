using log4net;
using MFW.LALLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFW.WF
{
    public partial class MainWindow : Form
    {
        #region Fields
        private ILog log = LogManager.GetLogger("MFWSample");
        private CallManager callManager = CallManager.GetInstance();
        private EventMonitor eventMonitor = EventMonitor.GetInstance();
        private DeviceManager deviceManager = DeviceManager.GetInstance();
        private LALProperties lalProperties = LALProperties.GetInstance();

        private IDictionary<int, CallWindow> callWindows = new Dictionary<int,CallWindow>();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        #region CallBacks
        private void callsChangedHandle(object send, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (var item in args.NewItems)
                        {
                            var call = item as Call;
                            if (null != call)
                            {
                                    var callWindow = new CallWindow(call);
                                    call.CallStateListener = callWindow;
                                    callWindows.Add(call.CallHandle, callWindow);
                                    callWindow.Closed += onCallWindowClosedHandle;
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (var item in args.OldItems)
                        {
                            var call = item as Call;
                            if (null != call)
                            {
                                if (callWindows.ContainsKey(call.CallHandle))
                                {
                                    var callWindow = callWindows[call.CallHandle];
                                    callWindow.Close();
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    {
                        foreach (var callWindow in callWindows.Values)
                        {
                            callWindow.Close();
                        }
                        callWindows.Clear();
                    }
                    break;
                case NotifyCollectionChangedAction.Move: break;
                case NotifyCollectionChangedAction.Replace: break;
            }
            gvCalls.DataSource = null;
            gvCalls.DataSource = callManager.CallList;
        }

        private void OnCallManagerPropertyChangedHandle(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "CurrentCall":  //当前通道变化
                    {
                        if(null != callManager.CurrentCall)
                        {
                            if(callWindows.ContainsKey(callManager.CurrentCall.CallHandle))
                            {
                                var callWindow = callWindows[callManager.CurrentCall.CallHandle];
                                if(null != callWindow)
                                {
                                    callWindow.Show();
                                }
                            }
                        }
                    }
                    break;
            }
            
        }
        private void onCallWindowClosedHandle(object sender, EventArgs args)
        {
            var callWindow = sender as CallWindow;
            if (null != callWindow)
            {
                var wnds = callWindows.Where(c => c.Value == callWindow).ToList();
                foreach (var wnd in wnds)
                {
                    var call = callManager.GetCall(wnd.Key);
                    if (null != call)
                    {
                        LAL.Hangup(call);
                        callManager.RemoveCall(call);
                    }
                    callWindows.Remove(wnd.Key);
                }
            }
        }
        private void deviceChangedHandle(object sender, NotifyCollectionChangedEventArgs args)
        {
            var audioInputDevices = deviceManager.GetDevicesByType(DeviceTypeEnum.AUDIO_INPUT);
                cbxAudioInput.DataSource = audioInputDevices;
            if(audioInputDevices.Count>0)
            {
                cbxAudioInput.SelectedIndex = 0;
            }
            var audioOutputDevices = deviceManager.GetDevicesByType(DeviceTypeEnum.AUDIO_OUTPUT);
            cbxAudioOutput.DataSource = audioOutputDevices;
            if(audioOutputDevices.Count>0)
            {
                cbxAudioOutput.SelectedIndex = 0;
            }
            var deviceInputDevices = deviceManager.GetDevicesByType(DeviceTypeEnum.VIDEO_INPUT);
            cbxVideoInput.DataSource = deviceInputDevices;
            if(deviceInputDevices.Count>0)
            {
                cbxVideoInput.SelectedIndex = 0;
                cbxVideoInput.Enabled = true;
                btnVideoCall.Enabled = true;
            }
            else
            {
                cbxVideoInput.Enabled = true;
                btnVideoCall.Enabled = false;
            }  
        }
        private void monitorEventHandle(Event evt)
        {
            switch (evt.EventType)
            {
                case EventTypeEnum.SIP_REGISTER_SUCCESS:  break;
                case EventTypeEnum.SIP_REGISTER_FAILURE: {
                        if(MessageBox.Show(this, "注册失败!", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error)== DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    } break;
                case EventTypeEnum.PLCM_MFW_SIP_REGISTER_UNREGISTERED: {
                        
                    } break;
                
                case EventTypeEnum.DEVICE_VIDEOINPUTCHANGED:break;   /* from MP */
                case EventTypeEnum.DEVICE_AUDIOINPUTCHANGED:break;  /* from MP */
                case EventTypeEnum.DEVICE_AUDIOOUTPUTCHANGED:break; /* from MP */
                case EventTypeEnum.DEVICE_MONITORINPUTSCHANGED:break;  /* from MP */
                
                case EventTypeEnum.NETWORK_CHANGED:break;
                case EventTypeEnum.MFW_INTERNAL_TIME_OUT: {
                        if (MessageBox.Show(this, "内部处理超时!", "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    } break;
                
                case EventTypeEnum.PLCM_MFW_IS_TALKING_STATUS_CHANGED:break;
                case EventTypeEnum.PLCM_MFW_CERTIFICATE_VERIFY:break;
                case EventTypeEnum.PLCM_MFW_TRANSCODER_FINISH:break;
                case EventTypeEnum.PLCM_MFW_ICE_STATUS_CHANGED:break;                
                case EventTypeEnum.PLCM_MFW_AUTODISCOVERY_STATUS_CHANGED:break;
            }
        }
        #endregion
        private void MainWindow_Load(object sender, EventArgs e)
        {
            callManager.CallsChanged += callsChangedHandle;
            callManager.PropertyChanged += OnCallManagerPropertyChangedHandle;
            eventMonitor.MonitorEvent += monitorEventHandle;
            eventMonitor.Dispatcher = this;
            deviceManager.DevicesChanged += deviceChangedHandle;
            LAL.GetDevices();
            gvCalls.AutoGenerateColumns = false;
        }

        #region Devices
        private void cbxAudioInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            lalProperties.SetProperty(PropertyEnum.AUDIO_INPUT_DEVICE, cbxAudioInput.SelectedValue.ToString());
        }

        private void cbxAudioOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            lalProperties.SetProperty(PropertyEnum.AUDIO_OUTPUT_DEVICE, cbxAudioOutput.SelectedValue.ToString());
        }

        private void cbxVideoInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            lalProperties.SetProperty(PropertyEnum.VIDEO_INPUT_DEVICE, cbxVideoInput.SelectedValue.ToString());
        }

        #endregion
        #region Call
        private void btnC_Click(object sender, EventArgs e)
        {
            txtCallee.Text = string.Empty;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var txt = txtCallee.Text;
            if (txt.Length > 0)
            {
                txtCallee.Text = txt.Substring(0, txt.Length - 1);
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "6";
        }

        private void btm7_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCallee.Text = txtCallee.Text + "9";
        }

        private void btnAudioCall_Click(object sender, EventArgs e)
        {
            var callAddr = txtCallee.Text.Trim();
            if (string.IsNullOrEmpty(callAddr))
            {
                MessageBox.Show("被呼叫方必须");
                return;
            }

            LAL.Hold(true);
            log.Info("[MainWindow]Dial a call " + CallModeEnum.PLCM_MFW_AUDIO_CALL);
            var errno = LAL.Dial(callAddr, CallModeEnum.PLCM_MFW_AUDIO_CALL);
            var msg = string.Empty;
            switch (errno)
            {
                case ErrorNumberEnum.PLCM_SAMPLE_OK:
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_INVALID_FORMAT:
                    msg = "Place call failed. Your input is in wrong format or contains illegal characters.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_IN_REGISTERING:
                    msg = "Place call failed. Registering to server, please try again after your registration is completed";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_EXCEED_MAXIMUM_CALLS:
                    msg = "Place call failed. Total Call Number Exceeds Maximum Allowable Value.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_ENCRYPTION_CONFIG:
                    msg = "Place call failed. Encryption settings do not match.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_NO_CONNECT:
                    msg = "Place call failed. No local network connection.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_HOST_UNKNOWN:
                    msg = "Place call failed. Your input can not be parsed into a valid address.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_EXIST:
                    msg = "Place call failed. The call of the far site already exist.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_GENERIC:
                    msg = "Place call failed. PLCM_MFW_ERR_GENERIC";
                    break;
                default:
                    msg = "Place call failed." + errno.ToString();
                    break;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (MessageBox.Show(this, msg, "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                }
            }
        }

        private void btnVideoCall_Click(object sender, EventArgs e)
        {
            var callAddr = txtCallee.Text.Trim();
            if (string.IsNullOrEmpty(callAddr))
            {
                MessageBox.Show("被呼叫方必须");
                return;
            }

            LAL.Hold(true);
            log.Info("[MainWindow]Dial a call " + CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL);
            var errno = LAL.Dial(callAddr, CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL);
            var msg = string.Empty;
            switch (errno)
            {
                case ErrorNumberEnum.PLCM_SAMPLE_OK:
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_INVALID_FORMAT:
                    msg = "Place call failed. Your input is in wrong format or contains illegal characters.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_IN_REGISTERING:
                    msg = "Place call failed. Registering to server, please try again after your registration is completed";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_EXCEED_MAXIMUM_CALLS:
                    msg = "Place call failed. Total Call Number Exceeds Maximum Allowable Value.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_ENCRYPTION_CONFIG:
                    msg = "Place call failed. Encryption settings do not match.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_NO_CONNECT:
                    msg = "Place call failed. No local network connection.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_HOST_UNKNOWN:
                    msg = "Place call failed. Your input can not be parsed into a valid address.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_CALL_EXIST:
                    msg = "Place call failed. The call of the far site already exist.";
                    break;
                case ErrorNumberEnum.PLCM_MFW_ERR_GENERIC:
                    msg = "Place call failed. PLCM_MFW_ERR_GENERIC";
                    break;
                default:
                    msg = "Place call failed." + errno.ToString();
                    break;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                if (MessageBox.Show(this, msg, "消息通知", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                }
            }
        }
        #endregion

        #region gridView
        private void gvCalls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var  dgv = (DataGridView)sender;
            if(e.ColumnIndex<0)
            {
                return;
            }
            if (dgv.Columns[e.ColumnIndex].Name == "CallState")
            {
                var status = string.Empty;
                var callState = (CallStateEnum)e.Value;
                switch(callState)
                {
                    case CallStateEnum.SIP_UNKNOWN:break;

                    case CallStateEnum.SIP_INCOMING_INVITE:   /* UAS received INVITE, from CC */
                        status = "呼入待接听";
                        break;
                    case CallStateEnum.SIP_INCOMING_CONNECTED:   /* from CC */
                        status = "呼入通话中";
                        break;
                    case CallStateEnum.SIP_CALL_HOLD:  /* local hold */
                        status = "主动保持";
                        break;
                    case CallStateEnum.SIP_CALL_HELD:  /* far site hold */
                        status = "被动保持";
                        break;
                    case CallStateEnum.SIP_CALL_DOUBLE_HOLD:  /* both far-site and local hold */
                        status = "双边保持";
                        break;

                    case CallStateEnum.SIP_OUTGOING_TRYING:         /* UAC get 100, from CC */
                        status = "呼出中";
                        break;
                    case CallStateEnum.SIP_OUTGOING_RINGING:       /* UAC get 180 from CC */
                        status = "呼出响铃";
                        break;                       //SIP_OUTGOING_ANSWERED,  /* UAC get 200 from CC */
                    case CallStateEnum.SIP_OUTGOING_CONNECTED:  /* from CC */
                        status = "呼出通话中";
                        break;
                    case CallStateEnum.SIP_CALL_CLOSED:
                        status = "通话关闭";
                        break;
                    case CallStateEnum.NULL_CALL:break;
                }
                e.Value = status;
                e.FormattingApplied = true;
            }
        }


        private void gvCalls_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if(e.RowIndex<0)
            {
                return;
            }
            var callState = (CallStateEnum)(dgv.Rows[e.RowIndex].Cells[1].Value);
            var columnName = dgv.Columns[e.ColumnIndex].Name;
            switch(columnName)
            {
                case "CallName":
                case "CallState":
                    e.Handled = false;
                    return;
            }

            var isHandled = true;
            switch (callState)
            {
                case CallStateEnum.SIP_UNKNOWN:
                    break;
                case CallStateEnum.SIP_INCOMING_INVITE:
                    if("btnAnswer"==columnName)
                    {
                        isHandled = false;
                    }
                    break;
                case CallStateEnum.SIP_INCOMING_CONNECTED:   /* from CC */
                    switch(columnName)
                    {
                        case "btnHold":isHandled = false; break;
                        case "btnHangup":isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_HOLD:  /* local hold */
                    switch (columnName)
                    {
                        case "btnResume": isHandled = false; break;
                        case "btnHangup": isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_HELD:  /* far site hold */
                    switch (columnName)
                    {
                        case "btnHold": isHandled = false; break;
                        case "btnHangup": isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_DOUBLE_HOLD:  /* both far-site and local hold */
                    switch (columnName)
                    {
                        case "btnResume": isHandled = false; break;
                        case "btnHangup": isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_OUTGOING_TRYING:         /* UAC get 100, from CC */
                    switch (columnName)
                    {
                        case "btnHangup": isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_OUTGOING_RINGING:       /* UAC get 180 from CC */
                    switch (columnName)
                    {
                        case "btnHangup": isHandled = false; break;
                    }
                    break;                       //SIP_OUTGOING_ANSWERED,  /* UAC get 200 from CC */
                case CallStateEnum.SIP_OUTGOING_CONNECTED:  /* from CC */
                    switch (columnName)
                    {
                        case "btnHold": isHandled = false; break;
                        case "btnHangup": isHandled = false; break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_CLOSED:
                    break;
                case CallStateEnum.NULL_CALL: break;
            }
            if (isHandled)
            {
                using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                }
            }
            e.Handled = isHandled;           
        }

        private void gvCalls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            var callState = (CallStateEnum)(dgv.Rows[e.RowIndex].Cells[1].Value);
            var columnName = dgv.Columns[e.ColumnIndex].Name;
            switch (columnName)
            {
                case "CallName":
                case "CallState":
                    return;
            }
            var callHandle = (int)(dgv.Rows[e.RowIndex].Cells[6].Value);
            switch (callState)
            {
                case CallStateEnum.SIP_UNKNOWN:
                    break;
                case CallStateEnum.SIP_INCOMING_INVITE:
                    if ("btnAnswer" == columnName)
                    {
                        Answer(callHandle);
                    }
                    break;
                case CallStateEnum.SIP_INCOMING_CONNECTED:   /* from CC */
                    switch (columnName)
                    {
                        case "btnHold": Hold(callHandle); break;
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_HOLD:  /* local hold */
                    switch (columnName)
                    {
                        case "btnResume": Resume(callHandle); break;
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_HELD:  /* far site hold */
                    switch (columnName)
                    {
                        case "btnHold": Hold(callHandle); break;
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_DOUBLE_HOLD:  /* both far-site and local hold */
                    switch (columnName)
                    {
                        case "btnResume": Resume(callHandle); break;
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_OUTGOING_TRYING:         /* UAC get 100, from CC */
                    switch (columnName)
                    {
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_OUTGOING_RINGING:       /* UAC get 180 from CC */
                    switch (columnName)
                    {
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;                       //SIP_OUTGOING_ANSWERED,  /* UAC get 200 from CC */
                case CallStateEnum.SIP_OUTGOING_CONNECTED:  /* from CC */
                    switch (columnName)
                    {
                        case "btnHold": Hold(callHandle); break;
                        case "btnHangup": Hangup(callHandle); break;
                    }
                    break;
                case CallStateEnum.SIP_CALL_CLOSED:
                    break;
                case CallStateEnum.NULL_CALL: break;
            }
        }       

        private void Answer(int callHandle)
        {
            var call = callManager.GetCall(callHandle);
            if (null != call)
            {
                if(!LAL.Answer(call,call.CallMode,true))
                {
                    MessageBox.Show(this, "接听失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void Hold(int callHandle)
        {
            var call = callManager.GetCall(callHandle);
            if (null != call)
            {
                if (!LAL.Hold(call))
                {
                    MessageBox.Show(this, "接听保持失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Resume(int callHandle)
        {
            var call = callManager.GetCall(callHandle);
            if (null != call)
            {
                if (!LAL.Resume(call))
                {
                    MessageBox.Show(this, "接听保持恢复失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Hangup(int callHandle)
        {
            var call = callManager.GetCall(callHandle);
            if (null != call)
            {
                if (!LAL.Hangup(call))
                {
                    MessageBox.Show(this, "挂断失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
