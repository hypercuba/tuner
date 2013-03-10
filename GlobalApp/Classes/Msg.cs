
using GlobalApp.Model;
namespace GlobalApp.Classes
{
    class Msg
    {
        public object Device { get; private set; }
        public string View { get; private set; }

        public Msg(string viewName, object device) 
        {
            View = viewName;
            Device = device;
        }
    }
}
