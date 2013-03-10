using System;
using System.Collections.Generic;
using GlobalApp.Model;
using System.Linq;
using NAudio.CoreAudioApi;

namespace GlobalApp.Design
{
    public class DesignDataService : IDataService
    {
        private static IList<Device> Devices
        {
            get
            {
                var devices = new List<Device>();
                foreach (MMDevice dev in new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                {
                    devices.Add(new Device { ID = dev.ID, Name = dev.FriendlyName });
                }
                return devices;
            }
        }

        private static Device DefaultDevice
        {
            get
            {
                return Devices.SingleOrDefault(i => i.ID == Guid.Empty.ToString()) ?? new Device { ID = Guid.Empty.ToString(), Name = "Is empty" };
            }
        }

        public void GetDefaultDevice(Action<Device, Exception> callback)
        {
            callback(DefaultDevice, null);
        }

        public static void GetAllDevices(Action<IList<Device>, Exception> callback)
        {
            callback(Devices, null);
        }
    }
}