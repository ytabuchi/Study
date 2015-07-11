using System;
using System.ComponentModel;

namespace XF_ManySwitches
{
    public class SwitchPageViewModel : INotifyPropertyChanged
    {
        public SwitchPageViewModel() {}

        bool _sw1Value;

        public bool Sw1Value
        {
            get { return _sw1Value; }
            set
            {
                if (_sw1Value != value)
                {
                    _sw1Value = value;
                    OnPropertyChanged("Sw1Value");
                }
            }
        }

        bool _sw2Value;

        public bool Sw2Value
        {
            get { return _sw2Value; }
            set
            {
                if (_sw2Value != value)
                {
                    _sw2Value = value;
                    OnPropertyChanged("Sw2Value");
                }
            }
        }

        bool _swAllValue;

        public bool SwAllValue
        {
            get { return _swAllValue; }
            set
            {
                if (_swAllValue != value)
                {
                    _swAllValue = value;
                    OnPropertyChanged("SwAllValue");
                }
                if (_sw1Value != value)
                {
                    _sw1Value = value;
                    OnPropertyChanged("Sw1Value");
                }
                if (_sw2Value != value)
                {
                    _sw2Value = value;
                    OnPropertyChanged("Sw2Value");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

