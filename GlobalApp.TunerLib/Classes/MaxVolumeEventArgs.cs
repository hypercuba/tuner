using System;
using System.Diagnostics;

namespace GlobalApp.TunerLib.Classes
{
    public class MaxVolumeEventArgs:EventArgs
    {
        public float Max { get; private set; }
        public float Min { get; private set; }

        [DebuggerStepThrough]
        public MaxVolumeEventArgs(float min, float max) 
        {
            Max = max;
            Min = min;
        } 
    }
}
