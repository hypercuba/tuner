using GlobalApp.TunerLib.Interfaces;
using NAudio.Mixer;
using NAudio.Wave;
using System;

namespace GlobalApp.TunerLib.Classes
{
    public class Input : IInput
    {
        private static Input _instanse;

        public static Input Instance
        {
            get
            {
                if (_instanse == null) _instanse = new Input();
                return _instanse;
            }
        }

        private float _maxValue;

        private float _minValue;

        public event EventHandler<MaxVolumeEventArgs> MaxCalculator;

        public WaveFormat Format
        {
            get
            {
                return _waveFormat;
            }
            set
            {
                _waveFormat = value;
                _notificationCount = value.SampleRate / 10;
            }
        }

        public double InputLevel
        {
            get { return _limitingInput; }
            set
            {
                _limitingInput = value;
                if (_volumeControl != null)
                {
                    _volumeControl.Percent = value;
                }
            }
        }

        private WaveIn _waveIn;
        private WaveFormat _waveFormat;
        private UnsignedMixerControl _volumeControl;
        private double _limitingInput = 100;
        private int _notificationCount;
        private int _counter;

        private Input()
        {
            Format = new WaveFormat(44100, 1);
        }

        public void Begin(int inputDevice)
        {
            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn = null;
            }
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = inputDevice;
            _waveIn.WaveFormat = Format;
            _waveIn.DataAvailable += _waveIn_DataAvailable;
            _waveIn.StartRecording();
            TryGetVolumeControl();
        }

        private void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                short sample = (short)(e.Buffer[i + 1] << 8 | e.Buffer[i]); //Pulse-code modulation. Преобразуем входной стереосигнал в моно
                SetTheVolumeIndicator(sample / 32768f);                    
            }
        }

        private void SetTheVolumeIndicator(float value)
        {
            _maxValue = Math.Max(_maxValue, value);
            _minValue = Math.Min(_minValue, value);
            _counter++;
            if (_counter >= _notificationCount && _notificationCount > 0)
            {
                if (MaxCalculator != null)
                {
                    MaxCalculator(this, new MaxVolumeEventArgs(_minValue, _maxValue));
                }
                Reset();
            }
        }

        private void Reset()
        {
            _counter = 0;
            _maxValue = 0;
            _minValue = 0;
        }

        private void TryGetVolumeControl()
        {
            int waveInDeviceNumber = _waveIn.DeviceNumber;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                var mixerLine = _waveIn.GetMixerLine();
                foreach (var control in mixerLine.Controls)
                {
                    if (VolumeAndMicLevelAssigned(control)) break;
                }
            }
            else
            {
                var mixer = new Mixer(waveInDeviceNumber);
                foreach (var destination in mixer.Destinations)
                {
                    if (destination.ComponentType == MixerLineComponentType.DestinationWaveIn)
                    {
                        foreach (var source in destination.Sources)
                        {
                            if (source.ComponentType == MixerLineComponentType.SourceMicrophone)
                            {
                                foreach (var control in source.Controls)
                                {
                                    if (VolumeAndMicLevelAssigned(control)) break;
                                }
                            }
                        }
                    }
                }
            }

        }

        private bool VolumeAndMicLevelAssigned(MixerControl control)
        {
            if (control.ControlType != MixerControlType.Volume) return false;
            _volumeControl = control as UnsignedMixerControl;
            InputLevel = _limitingInput;
            return true;
        }
    }
}
