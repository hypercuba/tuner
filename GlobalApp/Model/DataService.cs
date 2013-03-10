using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;

namespace GlobalApp.Model
{
    public class DataService : IDataService
    {
        private static IList<Device> Devices 
        {
            get 
            {
                var devices = new List<Device>();
                for (var devNumb = 0; devNumb < WaveIn.DeviceCount; devNumb++) 
                {
                    devices.Add(new Device{Name = WaveIn.GetCapabilities(devNumb).ProductName});
                }
                return devices;
            }
        }

        private static Device DefaultDevice 
        {
            get 
            {
                return Devices.First();
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