using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GlobalApp.Classes;
using GlobalApp.Model;
using GlobalApp.TunerLib.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace GlobalApp.ViewModel
{
    public class SoundDevicesVM : ViewModelBase
    {
        private float peak;

        public const string ViewName = "SoundDevicesView";

        public ICommand GoToTune 
        {
            get { return new RelayCommand(OpenTuneWindow); }
        }

        private void OpenTuneWindow() 
        {
            Messenger.Default.Send(new Msg(TunerViewModel.ViewName, SelectedDevice));
        }

        public const string SelectedDevicePropertyName = "SelectedDevice";

        private int _selectedDevice;

        public int SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }

            set
            {
                if (_selectedDevice == value) return;
                _selectedDevice = value;
                BeginMonitoring();
                RaisePropertyChanged(SelectedDevicePropertyName);
            }
        }

        public IList<Device> Devices 
        {
            get { return SystemInfo.Instance.Devices; }
        }

        public SoundDevicesVM()
        {
            BeginMonitoring();
        }

        public void BeginMonitoring() 
        {
            Input.Instance.Begin(_selectedDevice);
            Input.Instance.MaxCalculator += LevelChanged;
        }

        private void LevelChanged(object sender, MaxVolumeEventArgs e) 
        {
            peak = Math.Max(e.Max, Math.Abs(e.Min));
            RaisePropertyChanged("VolumeLevel");
        }

        public double InputLevel 
        { 
            get 
            { 
                return Input.Instance.InputLevel; 
            } 
            set 
            { 
                Input.Instance.InputLevel = value; 
            } 
        }

        public float VolumeLevel{get{return peak * 100;}}
    }
}