using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalApp.Model
{
    public interface IDataService
    {
        void GetDefaultDevice(Action<Device, Exception> callback);
    }
}
