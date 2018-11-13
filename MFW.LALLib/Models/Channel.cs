using System.ComponentModel;
using System.Windows;

namespace MFW.LALLib
{

    public class Channel: BaseModel
    {
        #region Constructors
        public Channel(int id ,bool isLocal) {
            this.ChannelID = id;
            this.IsLocal = isLocal;
        }
        public Channel(int id,string name,bool isLocal)
        {
            this.ChannelID = id;
            this.ChannelName = name;
            this.IsLocal = isLocal;
        }
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

        #region IsAudio
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
    }
}
