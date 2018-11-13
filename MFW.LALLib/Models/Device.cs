using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public class Device
    {
        private DeviceTypeEnum _deviceType;
        private string _deviceHandle;
        private string _deviceName;
        private IntPtr _appDeviceHandle;

        public Device(DeviceTypeEnum deviceType, string deviceHandle, string deviceName, IntPtr appDeviceHandle)
        {
            this._deviceType = deviceType;
            this._deviceHandle = deviceHandle;
            this._deviceName = deviceName;
            this._appDeviceHandle = appDeviceHandle;
        }

        public DeviceTypeEnum DeviceType { get { return this._deviceType; } }
        public string DeviceHandle { get { return this._deviceHandle; } }
        public string DeviceName { get { return this._deviceName; } }
        public IntPtr AppDeviceHandle { get { return this._appDeviceHandle; } }
    }
}
