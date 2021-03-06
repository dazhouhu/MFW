﻿using System;
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
        public Channel(Call call,int id ,ChannelType channelType,bool isActive=false) {
            this._call = call;
            this.ChannelID = id;
            this._channelType = channelType;
            this.IsActive = isActive;
        }
        public Channel(Call call, int id,string name, ChannelType channelType, bool isActive=false)
        {
            this._call = call;
            this.ChannelID = id;
            this.ChannelName = name;
            this._channelType = channelType;
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
                if (_channelName != value)
                {
                    _channelName = value;
                    NotifyPropertyChanged("ChannelName");
                }
            }
        }
        #endregion

        #region IsActive
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set {
                if (_isActive != value)
                {
                    _isActive = value;
                    NotifyPropertyChanged("IsActive");
                }
            }
        }
        #endregion

        #region IsVideo
        private bool _isVideo;
        public bool IsVideo
        {
            get { return _isVideo; }
            set {
                if(_isVideo != value){
                    _isVideo = value;
                    NotifyPropertyChanged("IsVideo");
                }
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

        #region ChannelType
        private ChannelType _channelType;
        public ChannelType ChannelType
        {
            get { return _channelType; }
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
                    IsVideo = true;
                    NotifyPropertyChanged("Size");
                }
            }
        }
        #endregion
    }
}
