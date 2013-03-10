using GlobalApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;

namespace GlobalApp.Classes
{
    [ManagementEntity(Singleton = true)]
    class SystemInfo
    {
        private static SystemInfo _instance = null;

        private SystemInfo() 
        {
            DataService.GetAllDevices((devices, exception) => 
            {
                if (exception == null) Devices = devices;
                else 
                    System.Windows.MessageBox.Show(exception.Message);
            });
        }

        public static SystemInfo Instance 
        {
            get 
            {
                return _instance ?? new SystemInfo();
            }
        }

        public IList<Device> Devices { get; private set; }
    }
}
