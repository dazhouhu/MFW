using log4net;
using MFW.LALLib;
using MFW.WF.UX;
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
    public partial class CallWindow : Form, ICallStateListener
    {
        #region Field
        private ILog log = LogManager.GetLogger("MFM.CallWindow");
        private DeviceManager deviceManager = DeviceManager.GetInstance();
        private Dictionary<int, ChannelView> channelViews = new Dictionary<int, ChannelView>();
        private Call _call = null;
        #endregion
        #region Constructors
        public CallWindow(Call call)
        {
            InitializeComponent();
            if(null == call)
            {
                throw new Exception("呼叫不存在");
            }
            _call = call;
            _call.PropertyChanged += OnCallPropertyChangedHandle;
            _call.Channels.CollectionChanged += OnChannelsCllectionChangedHandle;
        }
        #endregion
        
        
        #region CallBack
        private void OnCallPropertyChangedHandle(object sender , PropertyChangedEventArgs args)
        {
            switch(args.PropertyName)
            {
                case "CurrentChannel":  //当前通道变化
                    {
                        ViewRender();
                    }
                    break;
                case "IsAudioOnly": //语音视频变化
                    {
                        if (_call.IsAudioOnly)
                        {
                            btnCamera.Image = Properties.Resources.camera_mute;
                            btnCamera.Enabled = false;
                            this.btnShare.Enabled = false;
                            this.btnShare.Image = Properties.Resources.share_mute;
                        }
                        else
                        {
                            if (null == deviceManager.CurrentVideoInputDevice)
                            {
                                btnCamera.Image = Properties.Resources.camera_mute;
                                btnCamera.Enabled = false;
                                this.btnShare.Enabled = false;
                                this.btnShare.Image = Properties.Resources.share_mute;
                            }
                            else
                            {
                                btnCamera.Image = Properties.Resources.camera;
                                btnCamera.Enabled = true;
                                if (_call.IsContentSupported)
                                {
                                    this.btnShare.Enabled = true;
                                    this.btnShare.Image = Properties.Resources.share;
                                }
                                else
                                {
                                    this.btnShare.Enabled = false;
                                    this.btnShare.Image = Properties.Resources.share_mute;
                                }
                            }
                        }
                    }
                    break;
                case "IsContentSupported": //内容共享是否支持
                    {
                        if (_call.IsContentSupported)
                        {
                            btnShare.Image = Properties.Resources.share;
                            btnShare.Enabled = true;
                        }
                        else
                        {
                            btnShare.Image = Properties.Resources.share_mute;
                            btnShare.Enabled = false;
                        }
                    }
                    break;
            }

        }
        private void OnChannelsCllectionChangedHandle(object sender,  NotifyCollectionChangedEventArgs args)
        {
            #region ChannelView
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        foreach (var item in args.NewItems)
                        {
                            var channel = item as Channel;
                            if (null != channel)
                            {
                                var channelView = new ChannelView(channel);
                                channelViews.Add(channel.ChannelID, channelView);
                                pnlView.Controls.Add(channelView);
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (var item in args.OldItems)
                        {
                            var channel = item as Channel;
                            if (channelViews.ContainsKey(channel.ChannelID))
                            {
                                var channelView = channelViews[channel.ChannelID];
                                if (null != channelView)
                                {
                                    pnlView.Controls.Remove(channelView);
                                    channelView.Dispose();
                                }
                                channelViews.Remove(channel.ChannelID);
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:break;
                case NotifyCollectionChangedAction.Move: break;
                case NotifyCollectionChangedAction.Replace: break;
            }
            #endregion

            ViewRender();
        }
        #endregion

        #region View Render
        private void ViewRender()
        {
            var viewWidth = pnlView.Width;
            var viewHeight = pnlView.Height;
            var cellWidth = 200 * viewWidth / 800;
            var cellHeigth = 230 * viewHeight / 600;
            var i = 1;
            foreach(var channelViewKV in channelViews)
            {
                var view = channelViewKV.Value;
                if(channelViewKV.Key== _call.ActiveSpeakerId)
                {
                    view.Location = new Point(0,0);
                    view.Size = new Size(viewWidth - cellWidth, viewHeight);
                    view.SendToBack();
                }
                else
                {
                    view.Location = new Point(viewWidth - cellWidth, viewHeight - cellHeigth * (i + 1));
                    view.Size = new Size(cellWidth, cellHeigth);
                    view.BringToFront();
                }
            }            
        }
        #endregion

        #region Handles
        private void CallWindow_Load(object sender, EventArgs e)
        {
            #region Device Init
            if (null != deviceManager.CurrentAudioInputDevice)
            {
                this.btnMic.Enabled = true;
                this.btnMic.Image = Properties.Resources.mic;
                var volume= WrapperProxy.GetMicVolume();
                this.tbMicVolume.Value = volume;
                this.tbMicVolume.LostFocus += (obj, args) => { this.tbMicVolume.Hide(); };
            }
            else
            {
                this.btnMic.Enabled = false;
                this.btnMic.Image = Properties.Resources.mic_mute;
                this.tbMicVolume.Value = 0;
            }
            if (null != deviceManager.CurrentAudioOutputDevice)
            {
                this.btnSpeaker.Enabled = true;
                this.btnSpeaker.Image = Properties.Resources.speaker;
                var volume = WrapperProxy.GetSpeakerVolume();
                this.tbSpeakerVolume.Value = volume;
                this.tbSpeakerVolume.LostFocus += (obj, args) => { this.tbSpeakerVolume.Hide(); };
            }
            else
            {
                this.btnSpeaker.Enabled = false;
                this.btnSpeaker.Image = Properties.Resources.speaker_mute;
                this.tbSpeakerVolume.Value = 0;
            }
            if(null != deviceManager.CurrentVideoInputDevice)
            {
                this.btnCamera.Enabled = true;
                switch (_call.CallMode)
                {
                    case CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL:
                        {
                            this.btnCamera.Image = Properties.Resources.camera;
                            if (_call.IsContentSupported)
                            {
                                this.btnShare.Enabled = true;
                                this.btnShare.Image = Properties.Resources.share;
                            }
                            else
                            {
                                this.btnShare.Enabled = false;
                                this.btnShare.Image = Properties.Resources.share_mute;
                            }
                        }
                        break;
                    case CallModeEnum.PLCM_MFW_AUDIO_CALL:
                        {
                            this.btnCamera.Image = Properties.Resources.camera_mute;
                            this.btnShare.Enabled = false;
                            this.btnShare.Image = Properties.Resources.share_mute;
                        }
                        break;
                }
            }
            else
            {
                this.btnCamera.Enabled = false;
                this.btnCamera.Image = Properties.Resources.camera_mute;
                this.btnShare.Enabled = false;
                this.btnShare.Image = Properties.Resources.share_mute;
            }
            #endregion
        }
        private void btnMic_Click(object sender, EventArgs e)
        {
            tbMicVolume.Show();
            tbMicVolume.Focus();
            tbMicVolume.BringToFront();
            tbMicVolume.Left = btnMic.Left + 20;
            tbMicVolume.Top = this.Height-pnlToolBars.Height-158;
        }

        private void btnSpeaker_Click(object sender, EventArgs e)
        {
            tbSpeakerVolume.Show();
            tbSpeakerVolume.Focus();
            tbSpeakerVolume.BringToFront();
            tbSpeakerVolume.Left = btnSpeaker.Left + 20;
            tbSpeakerVolume.Top = this.Height - pnlToolBars.Height - 158;
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            switch (_call.CallMode)
            {
                case CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL:
                    {
                        msg = "确定要降级为语音通话吗?";
                    }break;
                case CallModeEnum.PLCM_MFW_AUDIO_CALL:
                    {
                        msg = "确定要升级为视频通话吗?";
                    }break;
            }
            if( MessageBox.Show(this, msg, "确认框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var callMode = _call.CallMode == CallModeEnum.PLCM_MFW_AUDIO_CALL ? CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL : CallModeEnum.PLCM_MFW_AUDIO_CALL;
                if(ErrorNumberEnum.PLCM_SAMPLE_OK != WrapperProxy.ChangeCallMode(_call.CallHandle, CallModeEnum.PLCM_MFW_AUDIO_CALL))
                {
                    MessageBox.Show(this, "处理失败!","消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {

        }

        private void btnAttender_Click(object sender, EventArgs e)
        {

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            moreMenu.Show(btnMore,new Point(0,0),ToolStripDropDownDirection.AboveRight);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tbMicVolume_ValueChanged(object sender, EventArgs e)
        {
            var volume = tbMicVolume.Value;
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != WrapperProxy.SetMicVolume(volume))
            {
                if(MessageBox.Show(this,"设置麦克风音量失败!","消息框", MessageBoxButtons.OK, MessageBoxIcon.Error)== DialogResult.OK)
                {
                    volume=WrapperProxy.GetMicVolume();
                    this.tbMicVolume.Value = volume;
                }
            }
            if(volume==0)
            {
                this.btnMic.Image = Properties.Resources.mic_mute;
            }
            else
            {
                this.btnMic.Image = Properties.Resources.mic;
            }
        }

        private void tbSpeakerVolume_ValueChanged(object sender, EventArgs e)
        {
            var volume = tbSpeakerVolume.Value;
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != WrapperProxy.SetSpeakerVolume(volume))
            {
                if (MessageBox.Show(this, "设置麦克风音量失败!", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    volume = WrapperProxy.GetSpeakerVolume();
                    this.tbSpeakerVolume.Value = volume;
                }
            }
            if (volume == 0)
            {
                this.btnSpeaker.Image = Properties.Resources.speaker_mute;
            }
            else
            {
                this.btnSpeaker.Image = Properties.Resources.speaker;
            }
        }
        private void CallWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch(_call.CallState)
            {
                case CallStateEnum.SIP_UNKNOWN:
                case CallStateEnum.SIP_CALL_CLOSED:
                case CallStateEnum.NULL_CALL:
                    break;
                case CallStateEnum.SIP_INCOMING_INVITE:
                case CallStateEnum.SIP_INCOMING_CONNECTED:
                case CallStateEnum.SIP_CALL_HOLD:
                case CallStateEnum.SIP_CALL_HELD:
                case CallStateEnum.SIP_CALL_DOUBLE_HOLD:
                case CallStateEnum.SIP_OUTGOING_TRYING:
                case CallStateEnum.SIP_OUTGOING_RINGING:
                case CallStateEnum.SIP_OUTGOING_CONNECTED:
                    break;
            }
        }
        #endregion

        #region ICallStateListener Implements
        public void handleEvent(Call call)
        {
            switch (call.CallEventState)
            {
                case CallEventStateEnum.UNKNOWN: { } break;
                case CallEventStateEnum.INCOMING_INVITE:                    /* UAS received an incoming call. */
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.IncomingSound, true, 2000);

                        var msg= string.Format("【{0}】呼入中，是否接听？", this._call.RemoteDisplayName);
                        if (MessageBox.Show(this,msg, "确认框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            LAL.Hold(true);
                            LAL.Hangup(true);
                            deviceManager.StopSound();
                            LAL.Answer(this._call, CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL, true);
                        }
                        else
                        {
                            LAL.Hangup(this._call);
                        }
                    }
                    break;
                case CallEventStateEnum.INCOMING_CLOSED:                    /* UAS received the call terminated. */
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.ClosedSound, false, 0);
                        if (MessageBox.Show(this, "呼叫已经关闭!", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
                case CallEventStateEnum.INCOMING_CONNECTED:                 /* UAS received the call connected. */
                    {
                        deviceManager.StopSound();
                    }
                    break;
                case CallEventStateEnum.INCOMING_HOLD: break;                      /* The call is holding by local site. */
                case CallEventStateEnum.INCOMING_HELD: break;                    /* The call is holding by far site. */
                case CallEventStateEnum.INCOMING_DOUBLE_HOLD: break;             /* The call is holding by both far site and local site. */
                case CallEventStateEnum.INCOMING_CALL_FARSITE_MIC_MUTE:     /* Far site has muted micphone. */
                    {
                        
                    }
                    break;
                case CallEventStateEnum.INCOMING_CALL_FARSITE_MIC_UNMUTE:   /* Far site has unmuted micphone. */
                    {
                        
                    }
                    break;
                case CallEventStateEnum.INCOMING_CONTENT:
                    break;
                    {
                        //  LAL.attachStreamToWindow(pnl.getContentWindow(), LAL.getActiveCall().getCallHandle(), MediaType.PLCM_MF_STREAM_CONTENT, 0, 0, 0);
                    }
                    break;
                case CallEventStateEnum.CONTENT_SENDING: break;
                case CallEventStateEnum.CONTENT_CLOSED:
                    {
                        // LAL.detachStreamFromWindow(MediaType.PLCM_MF_STREAM_CONTENT, 0, LAL.getActiveCall().getCallHandle()); /*detach content window*/
                    }
                    break;
                case CallEventStateEnum.CONTENT_UNSUPPORTED:break;
                case CallEventStateEnum.CONTENT_IDLE:break;

                case CallEventStateEnum.OUTGOING_RINGING:               /* UAC received far site is ringing.*/
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.RingingSound, true, 2000);

                        if (MessageBox.Show(this, "当前正呼叫中，确认要挂断吗？", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            LAL.Hangup(this._call);
                        }
                    }
                    break;
                case CallEventStateEnum.OUTGOING_FAILURE:               /* from CC */
                    {
                        deviceManager.StopSound();
                        if (MessageBox.Show(this, "呼叫失敗!", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
                case CallEventStateEnum.OUTGOING_CONNECTED:             /* from CC */
                    {
                        deviceManager.StopSound();
                        ViewRender();
                    }
                    break;

                case CallEventStateEnum.LOCAL_RESOLUTION_CHANGED: break;
                case CallEventStateEnum.REMOTE_RESOLUTION_CHANGED:break;

                case CallEventStateEnum.SIP_REGISTER_SUCCESS: break;           /* Register to sip server succeed. */
                case CallEventStateEnum.SIP_REGISTER_FAILURE: break;           /* Register to sip server failed. */
                case CallEventStateEnum.SIP_UNREGISTERED: break;               /* Unregister to sip server. */

                case CallEventStateEnum.NETWORK_CHANGED: break;                /* Network changed or lost.*/
                case CallEventStateEnum.CALL_AUDIO_ONLY_TRUE:  break;          /* The call is audio only.*/
                case CallEventStateEnum.CALL_AUDIO_ONLY_FALSE: break;          /* The call is not audio only.*/
                case CallEventStateEnum.MFW_INTERNAL_TIME_OUT: break;

                //SVC
                case CallEventStateEnum.REFRESH_ACTIVE_SPEAKER: {
                      //  ViewRender(); //重新布局
                    } break;
                case CallEventStateEnum.REFRESH_LAYOUT: {
                        this._call.ClearRemoteChannels();
                    } break;
                case CallEventStateEnum.CHANNEL_STATUS_UPDATE:break;
                case CallEventStateEnum.DISPLAYNAME_UPDATE:break;
                case CallEventStateEnum.CALL_MODE_UPGRADE_REQ: break;
                case CallEventStateEnum.DEVICE_VIDEOINPUT: break;
                case CallEventStateEnum.CERTIFICATE_VERIFY: break;
                case CallEventStateEnum.TRANSCODER_FINISH: break;
                case CallEventStateEnum.ICE_STATUS_CHANGED: break;
                case CallEventStateEnum.AUTODISCOVERY_SUCCESS: break;
                case CallEventStateEnum.AUTODISCOVERY_FAILURE: break;
                case CallEventStateEnum.AUTODISCOVERY_ERROR:
                    break;
                case CallEventStateEnum.SIP_CALL_TRYING:
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.RingingSound, true, 2000);

                        var msg = string.Format("呼叫 【{0}】 中...", this._call.DisplayCallName);

                        if(MessageBox.Show(this,msg,"消息框", MessageBoxButtons.OK, MessageBoxIcon.Information)== DialogResult.OK)
                        {
                            LAL.Hangup(this._call);
                        }
                    }
                    break;
            }
        }
        #endregion


    }
}
