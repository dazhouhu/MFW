using System;
using System.ComponentModel;
using System.Windows;

namespace MFW.LALLib
{

    public class Channel: BaseModel
    {
        #region Constructors
        public Channel(Call call)
        {
            this._call = call;

        }
        public Channel(Call call,int id ,bool isLocal,bool isActive=false) {
            this._call = call;
            this.ChannelID = id;
            this.IsLocal = isLocal;
            this.IsActive = isActive;
        }
        public Channel(Call call, int id,string name,bool isLocal,bool isActive=false)
        {
            this._call = call;
            this.ChannelID = id;
            this.ChannelName = name;
            this.IsLocal = isLocal;
            this.IsActive = isActive;
        }
        #endregion

        #region Field
        private Call _call;
        public Call Call { get { return _call; } }
        #endregion

        #region ChannelID
        private int _channelID = 0;
        public int ChannelID
        {
            get { return _channelID; }
            set {
                _channelID = value;
                NotifyPropertyChanged("ChannelID");
            }
        }
        #endregion

        #region ChannelName
        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set {
                _channelName = value;
                NotifyPropertyChanged("ChannelName");
            }
        }
        #endregion

        #region IsActive
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set {
                _isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }
        #endregion

        #region IsVideo
        private bool _isVideo;
        public bool IsVideo
        {
            get { return _isVideo; }
            set {
                _isVideo = value;
                NotifyPropertyChanged("IsVideo");
            }
        }
        #endregion

        #region IsAudio
        private bool _isAudio;
        public bool IsAudio
        {
            get { return _isAudio; }
            set {
                _isAudio = value;
                NotifyPropertyChanged("IsAudio");
            }
        }
        #endregion

        #region IsLocal
        private bool _isLocal;
        public bool IsLocal
        {
            get { return _isLocal; }
            set {
                _isLocal = value;
                NotifyPropertyChanged("IsLocal");
            }
        }
        #endregion

        #region Size
        private Tuple<int, int> _size = new Tuple<int, int>(400, 300);
        public Tuple<int, int> Size
        {
            get { return _size; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    NotifyPropertyChanged("Size");
                }
            }
        }
        #endregion
    }
}
