using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MFW.LALLib
{
    public class DeviceManager: BaseModel
    {
        #region Fields
        private ILog log = LogManager.GetLogger("DeviceManager");
        private ObservableCollection<Device> devices = new ObservableCollection<Device>();
        private Dictionary<string, Device> applications = new Dictionary<string, Device>();
        #endregion

        #region Constructors
        private static DeviceManager instance = null;
        public static DeviceManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DeviceManager();
                instance.devices.CollectionChanged += (sender, args) => {
                    instance.DevicesChanged?.Invoke(sender, args);
                };
            }
            return instance;
        }
        #endregion

        #region Events
        public event NotifyCollectionChangedEventHandler DevicesChanged;
        #endregion

        #region Device
        public void AddDevice(Device device)
        {
            if(device.DeviceName.Contains("none"))
            {
                return;
            }
            devices.Add(device);
            switch(device.DeviceType)
            {
                case DeviceTypeEnum.AUDIO_INPUT:
                case DeviceTypeEnum.AUDIO_OUTPUT:
                    {
                        var audioInput = GetDevicesByType(DeviceTypeEnum.AUDIO_INPUT).FirstOrDefault();
                        var audioOutput = GetDevicesByType(DeviceTypeEnum.AUDIO_OUTPUT).FirstOrDefault();
                        var inputHandle = audioInput?.DeviceHandle;
                        var outputHandle = audioOutput?.DeviceHandle;
                        if (null == CurrentVideoInputDevice && null != audioInput)
                        {
                            CurrentVideoInputDevice = audioInput;
                            WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                            WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                        }
                        if (null == CurrentAudioOutputDevice && null != outputHandle)
                        {
                            CurrentAudioOutputDevice = audioOutput;
                            WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                            WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                        }
                    }
                    break;
                case DeviceTypeEnum.VIDEO_INPUT:
                    {
                        if (null == CurrentAudioOutputDevice)
                        {
                            var video = GetDevicesByType(DeviceTypeEnum.VIDEO_INPUT).FirstOrDefault();
                            var videoHandle = video?.DeviceHandle;
                            if (null != videoHandle)
                            {
                                CurrentVideoInputDevice = video;
                                WrapperProxy.SetVideoDevice(videoHandle);
                            }
                        }
                    }break;
            }
        }
        public bool RemoveDevice(string deviceHandle)
        {
            for (int i = 0; i < devices.Count(); i++)
            {
                var device = devices[i];
                if (deviceHandle == device.DeviceHandle)
                {
                    devices.Remove(device);
                    switch (device.DeviceType)
                    {
                        case DeviceTypeEnum.AUDIO_INPUT:
                            {
                                if(CurrentAudioInputDevice== device)
                                {
                                    var audioInput = GetDevicesByType(DeviceTypeEnum.AUDIO_INPUT).FirstOrDefault();
                                    var audioOutput = GetDevicesByType(DeviceTypeEnum.AUDIO_OUTPUT).FirstOrDefault();
                                    var inputHandle = audioInput?.DeviceHandle;
                                    var outputHandle = audioOutput?.DeviceHandle;
                                    if (null == CurrentVideoInputDevice && null != audioInput)
                                    {
                                        CurrentVideoInputDevice = audioInput;
                                        WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                                        WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                                    }
                                    if (null == CurrentAudioOutputDevice && null != outputHandle)
                                    {
                                        CurrentAudioOutputDevice = audioOutput;
                                        WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                                        WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                                    }
                                }
                            }break;
                        case DeviceTypeEnum.AUDIO_OUTPUT:
                            {
                               if(CurrentAudioOutputDevice==device)
                                {
                                    var audioInput = GetDevicesByType(DeviceTypeEnum.AUDIO_INPUT).FirstOrDefault();
                                    var audioOutput = GetDevicesByType(DeviceTypeEnum.AUDIO_OUTPUT).FirstOrDefault();
                                    var inputHandle = audioInput?.DeviceHandle;
                                    var outputHandle = audioOutput?.DeviceHandle;
                                    if (null == CurrentVideoInputDevice && null != audioInput)
                                    {
                                        CurrentVideoInputDevice = audioInput;
                                        WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                                        WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                                    }
                                    if (null == CurrentAudioOutputDevice && null != outputHandle)
                                    {
                                        CurrentAudioOutputDevice = audioOutput;
                                        WrapperProxy.SetAudioDevice(inputHandle, outputHandle);
                                        WrapperProxy.SetAudioDeviceForRingtone(outputHandle);
                                    }
                                }                                
                            }
                            break;
                        case DeviceTypeEnum.VIDEO_INPUT:
                            {
                                if (CurrentVideoInputDevice == device)
                                {
                                    var video = GetDevicesByType(DeviceTypeEnum.VIDEO_INPUT).FirstOrDefault();
                                    var videoHandle = video?.DeviceHandle;
                                    WrapperProxy.SetVideoDevice(videoHandle);
                                }
                            }
                            break;
                    }
                    return true;
                }
            }
            return false;
        }
        public void ClearDevices()
        {
            devices.Clear();
        }

        public int GetDeviceCount()
        {
            return devices.Count;
        }
        public Device GetDevice(string deviceHandle)
        {
            return devices.Where(d => d.DeviceHandle == deviceHandle).FirstOrDefault();
        }

        public Device GetDeviceByName(string deviceName)
        {
            return devices.Where(d => d.DeviceName == deviceName).FirstOrDefault();
        }
        public ObservableCollection<Device> GetDevices()
        {
            return devices;
        }
        public IList<Device> GetDevicesByType(DeviceTypeEnum deviceType)
        {
            return devices.Where(d => d.DeviceType == deviceType).ToList();
        }
        #endregion

        #region Apps
        public void AddApp(Device app)
        {
            applications.Add(app.DeviceName, app);
        }
        public Device GetApp(string appName)
        {
            return applications[appName];
        }

        public void ClearApps()
        {
            applications.Clear();
        }
        #endregion

        #region CurrentDevice
        private Device _currentAudioInputDevice;
        public Device CurrentAudioInputDevice
        {
            get { return _currentAudioInputDevice; }
            set {
                _currentAudioInputDevice = value;
                NotifyPropertyChanged("CurrentAudioInputDevice");
            }
        }
        private Device _currentAudioOutputDevice;
        public Device CurrentAudioOutputDevice
        {
            get { return _currentAudioOutputDevice; }
            set {
                _currentAudioOutputDevice = value;
                NotifyPropertyChanged("CurrentAudioOutputDevice");
            }
        }
        private Device _currentVideoInputDevice;
        public Device CurrentVideoInputDevice
        {
            get { return _currentVideoInputDevice; }
            set {
                _currentVideoInputDevice = value;
                NotifyPropertyChanged("CurrentVideoInputDevice");
            }
        }
        
        #endregion

        #region Sound

        public static readonly string IncomingSound = System.Configuration.ConfigurationManager.AppSettings["IncomingSoundFilePath"];
        public static readonly string RingingSound = System.Configuration.ConfigurationManager.AppSettings["RingingSoundFilePath"];
        public static readonly string ClosedSound = System.Configuration.ConfigurationManager.AppSettings["ClosedSoundFilePath"];
        public static readonly string HoldSound = System.Configuration.ConfigurationManager.AppSettings["HoldSoundFilePath"];

        public void PlaySound(string filePath, bool loop, int interval)
        {
            log.Info("startPlayingAlert filePath:" + filePath);
            WrapperProxy.StartPlayAlert(filePath, loop, interval);
        }
        public void StopSound()
        {
            WrapperProxy.StopPlayAlert();
        }
        #endregion
    }
}
