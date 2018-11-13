using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace MFW.LALLib
{
    public class Call : BaseModel
    {
        #region CallHandle
        private int _callHandle=-1;
        public int CallHandle
        {
            get { return _callHandle; }
            set {
                _callHandle = value;
                NotifyPropertyChanged("CallHandle");
            }
        }
        #endregion
        #region DisplayCallName
        private string _callName;
        public string DisplayCallName
        {
            get { return _callName; }
            set {
                _callName = value;
                NotifyPropertyChanged("DisplayCallName");
            }
        }
        #endregion
        #region CallStateListener
        private ICallStateListener _callStateListener = null;
        public ICallStateListener CallStateListener { set { _callStateListener = value; } }
        #endregion

        #region CallState
        private CallStateEnum _callState;
        public CallStateEnum CallState
        {
            get { return _callState; }
            set {
                _callState = value;
                NotifyPropertyChanged("CallState");
            }
        }
        #endregion
        #region CallEventState
        private CallEventStateEnum _callEventState;
        public CallEventStateEnum CallEventState
        {
            get { return _callEventState; }
            set {
                _callEventState = value;
                if(null != _callStateListener)
                {
                    _callStateListener.handleEvent(this);
                }
            }
        }
        #endregion

        #region Reason
        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set {
                _reason = value;
                NotifyPropertyChanged("Reason");
            }
        }
        #endregion
        #region NetworkIP
        private string _networkIP;
        public string NetworkIP
        {
            get { return _networkIP; }
            set {
                _networkIP = value;
                NotifyPropertyChanged("NetworkIP");
            }
        }
        #endregion
        #region IsAudioOnly
        private bool _isAudioOnly = false;
        public bool IsAudioOnly
        {
            get { return _isAudioOnly; }
            set {
                _isAudioOnly = value;
                NotifyPropertyChanged("IsAudioOnly");
            }
        }
        #endregion
        #region IsMute
        private bool _isMute;
        public bool IsMute
        {
            get { return _isMute; }
            set {
                _isMute=value;
                NotifyPropertyChanged("IsMute");
            }
        }
        #endregion    
        #region
        private bool _isContentSupported = false;
        public bool IsContentSupported
        {
            get { return _isContentSupported; }
            set {
                _isContentSupported = value;
                NotifyPropertyChanged("IsContentSupported");
            }
        }
        #endregion
        #region Channels
        private ObservableCollection<Channel> _channels = new ObservableCollection<Channel>();
        public ObservableCollection<Channel> Channels
        {
            get { return _channels; }
        }

        public Channel GetChannel(int channelID)
        {
            return _channels.Where(c => c.ChannelID == channelID).FirstOrDefault();
        }
        public void AddChannel(int channelID)
        {
            var c = GetChannel(channelID);
            if (null == c)
            {
                var channel = new Channel(channelID, false);
                _channels.Add(channel);
                if(channelID==ActiveSpeakerId)
                {
                    CurrentChannel = channel;
                }
            }
        }
        public void AddChannel(Channel channel)
        {
            var c = GetChannel(channel.ChannelID);
            if (null == c)
            {
                _channels.Add(channel);
            }
            else
            {
                c.ChannelName = channel.ChannelName;
            }
        }
        public void RemoveChannel(int channelID)
        {
            var channel = GetChannel(channelID);
            if(null != channel)
            {
                _channels.Remove(channel);
            }
        }
        public void ClearChannels()
        {
            _channels.Clear();
        }

        public void SetChannelName (int channelID,string channelName)
        {
            var channel = GetChannel(channelID);
            if(null != channel)
            {
                channel.ChannelName = channelName;
            }
        }

        public Channel GetRemoteChanel()
        {
            return _channels.FirstOrDefault(c =>!c.IsLocal);
        }
        #endregion
        #region CurrentChannel
        public Channel CurrentChannel
        {
            get { return _channels.FirstOrDefault(f => f.IsActive); }
            set
            {
                var currentChannel = _channels.FirstOrDefault(f => f.IsActive);
                Channel channel = null;
                if (null != value)
                {
                    channel = GetChannel(value.ChannelID);
                }
                if (currentChannel != channel)
                {
                    if (null != currentChannel)
                    {
                        currentChannel.IsActive = false;
                    }
                    if (null != channel)
                    {
                        channel.IsActive = true;
                    }
                    NotifyPropertyChanged("CurrentChannel");
                }
            }
        }
        #endregion
        #region ChannelNumber
        private int _channelNumber;
        public int ChannelNumber {
            get { return _channelNumber; }
            set {
                _channelNumber = value;
                NotifyPropertyChanged("ChannelNumber");
            }
        }
        #endregion
        #region ActiveSpeakerId
        private int _activeSpeakerId = 0;
        public int ActiveSpeakerId {
            get { return null==CurrentChannel?0:CurrentChannel.ChannelID; }
            set{
                var channel = GetChannel(value);
                CurrentChannel = channel;
            }
        }
        #endregion

        #region LocalWidth
        public int LocalWidth { get; set; }
        #endregion
        #region LocalHeight
        public int LocalHeight { get; set; }
        #endregion
        #region RemoteWidth
        public int RemoteWidth { get; set; }
        #endregion
        #region RemoteHeight
        public int RemoteHeight { get; set; }
        #endregion
        #region ContentWidth
        public int ContentWidth{get;set;}
        #endregion
        #region ContentHeight
        public int ContentHeight { get; set; }
        #endregion
        #region CallMode
        private CallModeEnum _callMode;
        public CallModeEnum CallMode
        {
            get { return _callMode; }
            set
            {
                _callMode = value;
                NotifyPropertyChanged("CallMode");
            }
        }
        #endregion
        #region RemoteDisplayName
        private string _remoteDisplayName;
        public string RemoteDisplayName
        {
            get { return _remoteDisplayName; }
            set
            {
                _remoteDisplayName = value;
                NotifyPropertyChanged("RemoteDisplayName");
            }
        }
        #endregion
        #region EventRegID
        public string EventRegID { get; set; }
        #endregion

        #region IsCurrent
        private bool _isCurrent;
        public bool IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                _isCurrent = value;
                NotifyPropertyChanged("IsCurrent");
            }
        }
        #endregion

        private RecordAudioStreamCallback _recordAudioStreamCallback = null;
        #region Constructors
        public Call(int callHandle)
        {
            this.CallHandle = callHandle;
        }
        public Call(int callHandle, ICallStateListener callStateListener)
        {
            //_recordAudioStreamCallback = new RecordAudioStreamCallback(RecordAudioStreamCallbackF);
            this.CallHandle = callHandle;
            this._callStateListener = callStateListener;
        }
        #endregion
    }
}
