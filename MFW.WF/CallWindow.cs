﻿using log4net;
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
            this.Text = _call.DisplayCallName;
            _call.PropertyChanged += OnCallPropertyChangedHandle;
            _call.Channels.CollectionChanged += OnChannelsCllectionChangedHandle;
        }
        #endregion

        #region Properties
        private CallLayoutType _callLayoutType;
        public CallLayoutType CallLayoutType
        {
            get { return _callLayoutType; }
            set
            {
                if (_callLayoutType != value)
                {
                    _callLayoutType = value;
                    ViewRender();
                }
            }
        }
        private bool _muteCamera = true;
        public bool MuteCamera
        {
            get { return _muteCamera; }
            set
            {
                if(_muteCamera != value)
                {
                    _muteCamera = value;
                    btnCamera.Image = _muteCamera?Properties.Resources.camera_mute: Properties.Resources.camera;
                }
            }
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
                        if (null == deviceManager.CurrentVideoInputDevice)
                        {
                            btnCamera.Enabled = false;
                            btnCamera.Image = Properties.Resources.camera_mute;
                            this.btnShare.Enabled = false;
                            this.btnShare.Image = Properties.Resources.share_mute;
                        }
                        else
                        {                            
                            if (_call.IsAudioOnly)
                            {
                                btnCamera.Enabled = false; 
                                btnCamera.Image = Properties.Resources.camera_mute;
                                this.btnShare.Enabled = false;
                                this.btnShare.Image = Properties.Resources.share_mute;
                            }
                            else
                            {
                                btnCamera.Enabled = true;
                                btnCamera.Image = Properties.Resources.camera;
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
            if(channelViews.Count<=0)
            {
                return;
            }

            #region RendMode
            var mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_0X0;
            switch (CallLayoutType)
            {
                case CallLayoutType.VAS:
                    mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1X1;
                    break;
                case CallLayoutType.ContinuousPresence:
                    {
                        switch (_call.Channels.Count)
                        {
                            case 1:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1X1;
                                break;
                            case 2:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_2X1;
                                break;
                            case 3:
                            case 4:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_2X2;
                                break;
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_3X3;
                                break;
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_4X4;
                                break;
                        }
                    }
                    break;
                case CallLayoutType.Presentation:
                    {
                        switch (_call.Channels.Count)
                        {
                            case 1:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1X1;
                                break;
                            case 2:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_2X1;
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P5;
                                break;
                            case 7:
                            case 8:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P7;
                                break;
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P12;
                                break;
                            case 14:
                            case 15:
                            case 16:
                                mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P16;
                                break;
                        }
                    }
                    break;
                case CallLayoutType.Sigle:
                    mode = VideoLayoutModeEnum.SVC_LAYOUT_MODE_0X0;
                    break;
              }
            #endregion

            var viewWidth = pnlView.Width;
            var viewHeight = pnlView.Height-80;
            var ratioWidth =  320;
            var ratioHeight = 220;
            var cellWidth = 320 ;
            var cellHeigth = 220;

            var activeView = channelViews.Where(v => v.Key == _call.ActiveSpeakerId).Select(v=>v.Value).FirstOrDefault();
            if(null == activeView)
            {
                _call.ActiveSpeakerId = channelViews.Last().Key;
            }
            var notActiveViews= channelViews.Where(v => v.Key != _call.ActiveSpeakerId).Select(v=>v.Value).ToList();

            switch(mode)
            {
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_0X0:
                    {
                        activeView.Location = new Point(0, 0);
                        activeView.Size = new Size(viewWidth, viewHeight);
                        activeView.Visible = true;
                        activeView.IsShowBar = false;
                        activeView.BringToFront();

                        foreach(var notActiveView in notActiveViews)
                        {
                            notActiveView.Visible = false;
                            notActiveView.IsShowBar = true;
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_1X1:
                    {
                        activeView.Location = new Point(0, 0);
                        activeView.Size = new Size(viewWidth, viewHeight);
                        activeView.Visible = true;
                        activeView.IsShowBar = false;
                        activeView.SendToBack();

                        var i = 1;
                        foreach (var notActiveView in notActiveViews)
                        {
                            notActiveView.Location = new Point(viewWidth - cellWidth, viewHeight - cellHeigth * i);
                            notActiveView.Size = new Size(cellWidth, cellHeigth);
                            notActiveView.Visible = true;
                            notActiveView.IsShowBar = true;
                            notActiveView.BringToFront();
                            i++;
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_2X1:
                    {
                        cellWidth = viewWidth / 2;
                        cellHeigth = cellWidth * ratioHeight / ratioWidth;
                        int x = 0;
                        int y = (viewHeight - cellHeigth) / 2;
                        foreach (var view in channelViews.Values)
                        {
                            view.Location = new Point(x, y);
                            view.Size = new Size(cellWidth, cellHeigth);
                            view.Visible = true;
                            view.IsShowBar = true;
                            view.BringToFront();
                            x = x + cellWidth;
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_2X2:
                    {
                        cellHeigth = viewHeight / 2;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int initX = (viewWidth - 2*cellWidth) / 2;
                        var x = initX;
                        int y = 0;
                        var i = 0;
                        foreach (var view in channelViews.Values)
                        {
                            view.Location = new Point(x, y);
                            view.Size = new Size(cellWidth, cellHeigth);
                            view.Visible = true;
                            view.IsShowBar = true;
                            view.BringToFront();
                            x = x + cellWidth;
                            i++;
                            if (i % 2 == 0)
                            {
                                x = initX;
                                y = y + cellHeigth;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_3X3:
                    {
                        cellHeigth = viewHeight / 3;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int initX = (viewWidth - 3*cellWidth) / 2;
                        var x = initX;
                        int y = 0;
                        var i = 0;
                        foreach (var view in channelViews.Values)
                        {
                            view.Location = new Point(x, y);
                            view.Size = new Size(cellWidth, cellHeigth);
                            view.Visible = true;
                            view.IsShowBar = true;
                            view.BringToFront();
                            x = x + cellWidth;
                            i++;
                            if (i % 3 == 0)
                            {
                                x = initX;
                                y = y + cellHeigth;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_4X4:
                    {
                        cellHeigth = viewHeight / 4;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int initX = (viewWidth - 4*cellWidth) / 2;
                        var x = initX;
                        int y = 0;
                        var i = 0;
                        foreach (var view in channelViews.Values)
                        {
                            view.Location = new Point(x, y);
                            view.Size = new Size(cellWidth, cellHeigth);
                            view.Visible = true;
                            view.IsShowBar = true;
                            view.BringToFront();
                            x = x + cellWidth;
                            i++;
                            if (i % 4 == 0)
                            {
                                x = initX;
                                y = y + cellHeigth;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P5:
                    {
                        cellHeigth = viewHeight / 3;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int x = (viewWidth - 3*cellWidth) / 2;
                        int y = 0;
                        activeView.Location = new Point(x, y);
                        activeView.Size = new Size(cellWidth*2, cellHeigth*2);
                        activeView.Visible = true;
                        activeView.IsShowBar = true;
                        activeView.BringToFront();
                        for (var i = 0; i < notActiveViews.Count; i++)
                        {
                            var notActiveView = notActiveViews[i];
                            notActiveView.Size = new Size(cellWidth, cellHeigth);
                            notActiveView.Visible = true;
                            notActiveView.IsShowBar = true;
                            notActiveView.BringToFront();
                            switch (i)
                            {
                                case 0: notActiveView.Location = new Point(x + 2 * cellWidth, y); break;
                                case 1: notActiveView.Location = new Point(x + 2 * cellWidth, y + cellHeigth); break;

                                case 2: notActiveView.Location = new Point(x, y + 2 * cellHeigth); break;
                                case 3: notActiveView.Location = new Point(x + cellWidth, y + 2 * cellHeigth); break;
                                case 4: notActiveView.Location = new Point(x + 2 * cellWidth, y + 2 * cellHeigth); break;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P7:
                    {
                        cellHeigth = viewHeight / 4;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int x = (viewWidth - 4*cellWidth) / 2;
                        int y = 0;
                        activeView.Location = new Point(x, y);
                        activeView.Size = new Size(cellWidth*3, cellHeigth*3);
                        activeView.Visible = true;
                        activeView.IsShowBar = true;
                        activeView.BringToFront();
                        for (var i = 0; i < notActiveViews.Count; i++)
                        {
                            var notActiveView = notActiveViews[i];
                            notActiveView.Size = new Size(cellWidth, cellHeigth);
                            notActiveView.Visible = true;
                            notActiveView.IsShowBar = true;
                            notActiveView.BringToFront();
                            switch (i)
                            {
                                case 0: notActiveView.Location = new Point(x + 3 * cellWidth, y); break;
                                case 1: notActiveView.Location = new Point(x + 3* cellWidth, y + cellHeigth); break;
                                case 2: notActiveView.Location = new Point(x + 3 * cellWidth, y + 2*cellHeigth); break;

                                case 3: notActiveView.Location = new Point(x, y + 3 * cellHeigth); break;
                                case 4: notActiveView.Location = new Point(x + cellWidth, y + 3 * cellHeigth); break;
                                case 5: notActiveView.Location = new Point(x + 2 * cellWidth, y + 3* cellHeigth); break;
                                case 6: notActiveView.Location = new Point(x + 3 * cellWidth, y + 3 * cellHeigth); break;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P12:
                    {
                        cellHeigth = viewHeight / 4;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int x = (viewWidth - 4*cellWidth) / 2;
                        int y = 0;
                        activeView.Location = new Point(x+cellWidth, y+cellHeigth);
                        activeView.Size = new Size(cellWidth*2, cellHeigth*2);
                        activeView.Visible = true;
                        activeView.IsShowBar = true;
                        activeView.BringToFront();
                        for(var i=0;i< notActiveViews.Count; i++)
                        {
                            var notActiveView = notActiveViews[i];
                            notActiveView.Size = new Size(cellWidth, cellHeigth);
                            notActiveView.Visible = true;
                            notActiveView.IsShowBar = true;
                            notActiveView.BringToFront();
                            switch (i)
                            {
                                case 0: notActiveView.Location = new Point(x, y); break;
                                case 1: notActiveView.Location = new Point(x + cellWidth, y); break;
                                case 2: notActiveView.Location = new Point(x + 2 * cellWidth, y); break;
                                case 3: notActiveView.Location = new Point(x + 3 * cellWidth, y); break;

                                case 4: notActiveView.Location = new Point(x, y + cellHeigth); break;
                                case 5: notActiveView.Location = new Point(x + 3 * cellWidth, y + cellHeigth); break;

                                case 6: notActiveView.Location = new Point(x, y + 2 * cellHeigth); break;
                                case 7: notActiveView.Location = new Point(x + 3 * cellWidth, y + 2 * cellHeigth); break;

                                case 8: notActiveView.Location = new Point(x, y + 3 * cellHeigth); break;
                                case 9: notActiveView.Location = new Point(x + cellWidth, y + 3 * cellHeigth); break;
                                case 10: notActiveView.Location = new Point(x + 2 * cellWidth, y + 3 * cellHeigth); break;
                                case 11: notActiveView.Location = new Point(x + 3 * cellWidth, y + 3 * cellHeigth); break;
                            }
                        }
                    }
                    break;
                case VideoLayoutModeEnum.SVC_LAYOUT_MODE_1P16:
                    {
                        cellHeigth = viewHeight / 5;
                        cellWidth = cellHeigth * ratioHeight / ratioHeight;
                        int x = (viewWidth - 5*cellWidth) / 2;
                        int y = 0;
                        activeView.Location = new Point(x + cellWidth, y + cellHeigth);
                        activeView.Size = new Size(cellWidth * 3, cellHeigth * 3);
                        activeView.Visible = true;
                        activeView.IsShowBar = true;
                        activeView.BringToFront();
                        for (var i = 0; i < notActiveViews.Count; i++)
                        {
                            var notActiveView = notActiveViews[i];
                            notActiveView.Size = new Size(cellWidth, cellHeigth);
                            notActiveView.Visible = true;
                            notActiveView.IsShowBar = true;
                            notActiveView.BringToFront();
                            switch (i)
                            {
                                case 0: notActiveView.Location = new Point(x, y); break;
                                case 1: notActiveView.Location = new Point(x + cellWidth, y); break;
                                case 2: notActiveView.Location = new Point(x + 2 * cellWidth, y); break;
                                case 3: notActiveView.Location = new Point(x + 3 * cellWidth, y); break;
                                case 4: notActiveView.Location = new Point(x + 4 * cellWidth, y); break;

                                case 5: notActiveView.Location = new Point(x, y + cellHeigth); break;
                                case 6: notActiveView.Location = new Point(x + 4 * cellWidth, y + cellHeigth); break;

                                case 7: notActiveView.Location = new Point(x, y + 2 * cellHeigth); break;
                                case 8: notActiveView.Location = new Point(x + 4 * cellWidth, y + 2 * cellHeigth); break;

                                case 9: notActiveView.Location = new Point(x, y + 3* cellHeigth); break;
                                case 10: notActiveView.Location = new Point(x + 4 * cellWidth, y + 3 * cellHeigth); break;

                                case 11: notActiveView.Location = new Point(x, y + 4 * cellHeigth); break;
                                case 12: notActiveView.Location = new Point(x + cellWidth, y + 4 * cellHeigth); break;
                                case 13: notActiveView.Location = new Point(x + 2 * cellWidth, y + 4 * cellHeigth); break;
                                case 14: notActiveView.Location = new Point(x + 3 * cellWidth, y + 4 * cellHeigth); break;
                                case 15: notActiveView.Location = new Point(x + 4 * cellWidth, y + 4 * cellHeigth); break;
                            }
                        }
                    }
                    break;
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

            var localChannel = new Channel(this._call, 0, ChannelType.Local, false)
            {
                ChannelName = "本地视频",
                IsAudio = false,
                IsVideo = false
            };
            this._call.AddChannel(localChannel);

            _muteCamera = true;
            btnCamera.Image = Properties.Resources.camera_mute;


            WrapperProxy.GetApplicationInfo();
        }
        private void btnMic_Click(object sender, EventArgs e)
        {
            tbMicVolume.Show();
            tbMicVolume.Focus();
            tbMicVolume.BringToFront();
            tbMicVolume.Left = btnMic.Left + 10;
            tbMicVolume.Top = this.Height-pnlToolBars.Height-158;
        }

        private void btnSpeaker_Click(object sender, EventArgs e)
        {
            tbSpeakerVolume.Show();
            tbSpeakerVolume.Focus();
            tbSpeakerVolume.BringToFront();
            tbSpeakerVolume.Left = btnSpeaker.Left + 10;
            tbSpeakerVolume.Top = this.Height - pnlToolBars.Height - 158;
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            if(true==MuteCamera)
            {
                if (ErrorNumberEnum.PLCM_SAMPLE_OK == WrapperProxy.StartCamera())
                {
                    MuteCamera = false;
                }
                else
                {
                    UXMessageMask.ShowMessage(this, false, "启动摄像头失败！", MessageBoxButtonsType.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (ErrorNumberEnum.PLCM_SAMPLE_OK == WrapperProxy.StopCamera())
                {
                    MuteCamera = true;
                }
                else
                {
                    UXMessageMask.ShowMessage(this, false, "关闭摄像头失败！", MessageBoxButtonsType.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {

            var contentSelectWin = new ContentSelectPanel() {
                Monitors = deviceManager.GetDevicesByType(DeviceTypeEnum.DEV_MONITOR),
                Apps = deviceManager.GetDevicesByType(DeviceTypeEnum.APPLICATIONS),
                OKAction = (type,format,monitor,app) => {
                    switch (type)
                    {
                        case "Monitor":
                            {
                                var errno = WrapperProxy.StartShareContent(_call.CallHandle, monitor, app);
                                if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
                                {
                                    MessageBox.Show(this, "共享内容失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            break;
                        case "BFCP":
                            {
                                var width = Screen.PrimaryScreen.Bounds.Width;
                                var height = Screen.PrimaryScreen.Bounds.Height;
                                WrapperProxy.SetContentBuffer(format, width, height);
                                var errno = WrapperProxy.StartBFCPContent(_call.CallHandle);
                                if (errno != ErrorNumberEnum.PLCM_SAMPLE_OK)
                                {
                                    MessageBox.Show(this, "共享内容失败！", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                            break;
                    }
                    return true;
                },
                OnCancel = ()=>{ }
            };
            UXMessageMask.ShowForm(this, contentSelectWin);
        }

        private void btnAttender_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "实现中", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if(0== volume)
            {
                if(ErrorNumberEnum.PLCM_SAMPLE_OK== WrapperProxy.MuteMic(_call.CallHandle, true))
                {
                    this.btnMic.Image = Properties.Resources.mic_mute;
                }
            }
            else
            {
                if (ErrorNumberEnum.PLCM_SAMPLE_OK == WrapperProxy.MuteMic(_call.CallHandle, false))
                {
                    this.btnMic.Image = Properties.Resources.mic;
                }
            }
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != WrapperProxy.SetMicVolume(volume))
            {
                Action okAction = () =>
                {
                    volume = WrapperProxy.GetMicVolume();
                    this.tbMicVolume.Value = volume;
                };
                UXMessageMask.ShowMessage(this, false, "设置麦克风音量失败!", MessageBoxButtonsType.OK, MessageBoxIcon.Error
                                            , okAction);
            }
        }

        private void tbSpeakerVolume_ValueChanged(object sender, EventArgs e)
        {
            var volume = tbSpeakerVolume.Value;
            if (0 == volume)
            {
                if (ErrorNumberEnum.PLCM_SAMPLE_OK == WrapperProxy.MuteSpeaker( true))
                {
                    this.btnSpeaker.Image = Properties.Resources.speaker_mute;
                }
            }
            else
            {
                if (ErrorNumberEnum.PLCM_SAMPLE_OK == WrapperProxy.MuteSpeaker(false))
                {
                    this.btnSpeaker.Image = Properties.Resources.speaker;
                }
            }
            if (ErrorNumberEnum.PLCM_SAMPLE_OK != WrapperProxy.SetSpeakerVolume(volume))
            {
                if (MessageBox.Show(this, "设置麦克风音量失败!", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    volume = WrapperProxy.GetSpeakerVolume();
                    this.tbSpeakerVolume.Value = volume;
                }
            }
        }
        private void CallWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool canDirectClose = false;
            switch(_call.CallState)
            {
                case CallStateEnum.SIP_UNKNOWN:
                case CallStateEnum.SIP_CALL_CLOSED:
                case CallStateEnum.NULL_CALL:
                    canDirectClose = true;
                    break;
                case CallStateEnum.SIP_INCOMING_INVITE:
                case CallStateEnum.SIP_INCOMING_CONNECTED:
                case CallStateEnum.SIP_CALL_HOLD:
                case CallStateEnum.SIP_CALL_HELD:
                case CallStateEnum.SIP_CALL_DOUBLE_HOLD:
                case CallStateEnum.SIP_OUTGOING_TRYING:
                case CallStateEnum.SIP_OUTGOING_RINGING:
                case CallStateEnum.SIP_OUTGOING_CONNECTED:
                    canDirectClose = false;
                    break;
            }
            if(!canDirectClose)
            {
                Action okAction = () => {
                    LAL.Hangup(_call);
                    e.Cancel = false;
                };
                Action cancelAction = () =>
                {
                    e.Cancel = true;
                };
                UXMessageMask.ShowMessage(this, true, "当前通话连通中.\r\n确认要关闭退出吗？", MessageBoxButtonsType.OKCancel, MessageBoxIcon.Question
                                        , okAction, cancelAction);
            }
        }

        private void CallWindow_SizeChanged(object sender, EventArgs e)
        {
            ViewRender();
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
                        Action answerAction = () => {
                            LAL.Hold(true);
                            LAL.Hangup(true);
                            deviceManager.StopSound();
                            LAL.Answer(this._call, CallModeEnum.PLCM_MFW_AUDIOVIDEO_CALL, true);
                        };
                        Action hangupAction = () =>
                        {
                            LAL.Hangup(this._call);
                        };
                        UXMessageMask.ShowMessage(this, false, msg, MessageBoxButtonsType.AnswerHangup, MessageBoxIcon.Question
                                                    , answerAction,hangupAction);
                    }
                    break;
                case CallEventStateEnum.INCOMING_CLOSED:                    /* UAS received the call terminated. */
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.ClosedSound, false, 0);
                        Action okAction = () =>
                        {
                            this.Close();
                        };
                        UXMessageMask.ShowMessage(this, false, "呼叫已经关闭!", MessageBoxButtonsType.OK, MessageBoxIcon.Information
                                                    , okAction);
                    }
                    break;
                case CallEventStateEnum.INCOMING_CONNECTED:                 /* UAS received the call connected. */
                    {
                        deviceManager.StopSound();
                        ViewRender();
                        UXMessageMask.HideMessage(this);
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
                    {                  
                    }
                    break;
                case CallEventStateEnum.CONTENT_SENDING:
                    {
                        log.Info("CONTENT_SENDING");
                    } break;
                case CallEventStateEnum.CONTENT_CLOSED:
                    {
                        var contentChannel = _call.Channels.FirstOrDefault(c => c.ChannelType == ChannelType.Content);
                        if (null != contentChannel)
                        {
                            this._call.RemoveChannel(contentChannel.ChannelID);
                        }
                        // LAL.detachStreamFromWindow(MediaType.PLCM_MF_STREAM_CONTENT, 0, LAL.getActiveCall().getCallHandle()); /*detach content window*/
                    }
                    break;
                case CallEventStateEnum.CONTENT_UNSUPPORTED:break;
                case CallEventStateEnum.CONTENT_IDLE:break;

                case CallEventStateEnum.OUTGOING_RINGING:               /* UAC received far site is ringing.*/
                    {
                        deviceManager.StopSound();
                        deviceManager.PlaySound(DeviceManager.RingingSound, true, 2000);

                        Action hangupAction = () =>
                        {
                            this.Close();
                        };
                        UXMessageMask.ShowMessage(this, false, "当前正呼叫中...", MessageBoxButtonsType.Hangup, MessageBoxIcon.Information
                                                    , hangupAction);
                    }
                    break;
                case CallEventStateEnum.OUTGOING_FAILURE:               /* from CC */
                    {
                        deviceManager.StopSound();

                        Action okAction = () =>
                        {
                            this.Close();
                        };
                        UXMessageMask.ShowMessage(this, false, "呼叫失敗!", MessageBoxButtonsType.OK, MessageBoxIcon.Error
                                                    , okAction);
                    }
                    break;
                case CallEventStateEnum.OUTGOING_CONNECTED:             /* from CC */
                    {
                        deviceManager.StopSound();
                        ViewRender();
                        UXMessageMask.HideMessage(this);
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

                        Action hangupAction = () => { LAL.Hangup(this._call); };
                        UXMessageMask.ShowMessage(this, false, "呼叫中...", MessageBoxButtonsType.Hangup, MessageBoxIcon.Information
                                                    , hangupAction);
                    }
                    break;
            }
        }

        #endregion

        #region Menu

        private void menuItemDTMF_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "暂时不实现", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void menuItemFECC_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "暂时不实现", "消息框", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void menuItemP_Click(object sender, EventArgs e)
        {
            this.CallLayoutType = CallLayoutType.Presentation;
        }

        private void menuItemVAS_Click(object sender, EventArgs e)
        {
            this.CallLayoutType = CallLayoutType.VAS;
        }

        private void menuItemCP_Click(object sender, EventArgs e)
        {
            this.CallLayoutType = CallLayoutType.ContinuousPresence;
        }
        private void menuItemSingle_Click(object sender, EventArgs e)
        {
            this.CallLayoutType = CallLayoutType.Sigle;
        }
        #endregion

    }


    public enum CallLayoutType
    {
        Presentation,
        VAS,
        ContinuousPresence,
        Sigle
    }
    enum VideoLayoutModeEnum
    {
        SVC_LAYOUT_MODE_0X0,
        SVC_LAYOUT_MODE_1X1, 
        SVC_LAYOUT_MODE_2X1, 
        SVC_LAYOUT_MODE_2X2, 
        SVC_LAYOUT_MODE_1P5,
        SVC_LAYOUT_MODE_3X3, 
        SVC_LAYOUT_MODE_4X4,
        SVC_LAYOUT_MODE_1P7, 
        SVC_LAYOUT_MODE_1P12,
        SVC_LAYOUT_MODE_1P16  
    }
}
