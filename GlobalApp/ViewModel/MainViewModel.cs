using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GlobalApp.Classes;
using GlobalApp.Interfaces;
using GlobalApp.Model;
using GlobalApp.Views;
using NAudio.Wave;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace GlobalApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Dictionary<string, FrameworkElement> Views;
        private IDataService _dataService;

        public MainViewModel(IDataService dataService) 
        {
            Messenger.Default.Register<Msg>(this, (message) => Navigate(message));
            Views = new Dictionary<string, FrameworkElement>();
            AddView(SoundDevicesVM.ViewName, new SoundDevicesView(), new SoundDevicesVM());
            AddView(TunerViewModel.ViewName, new TunerView(), new TunerViewModel());
            _dataService = dataService;
            Messenger.Default.Send(new Msg(SoundDevicesVM.ViewName, null));
        }

        private void Navigate(Msg message) 
        {
            this.CurrentView = Views[message.View];
        }

        private void AddView(string name, FrameworkElement view, ViewModelBase viewModel) 
        {
            view.DataContext = viewModel;
            Views.Add(name, view);
        }

        public const string CurrentViewPropertyName = "CurrentView";

        private FrameworkElement _currentView;
        public FrameworkElement CurrentView 
        {
            get { return _currentView; }
            set 
            {
                if (value == _currentView) return;
                _currentView = value;
                RaisePropertyChanged(CurrentViewPropertyName);
            }
        }
        
    }
}