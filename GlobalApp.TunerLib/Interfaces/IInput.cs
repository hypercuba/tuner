using GlobalApp.TunerLib.Classes;
using NAudio.Wave;
using System;

namespace GlobalApp.TunerLib.Interfaces
{
    public interface IInput
    {
        void Begin(int inputDevice);
        event EventHandler<MaxVolumeEventArgs> MaxCalculator;
        WaveFormat Format { get; set; }
        double InputLevel { get; set; }
    }
}
