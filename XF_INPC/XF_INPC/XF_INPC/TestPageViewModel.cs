using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace XF_INPC
{
    class TestPageViewModel : INotifyPropertyChanged
    {
        private bool _boolValue;

        public bool BoolValue
        {
            get { return _boolValue; }
            set
            {
                if (_boolValue != value)
                {
                    _boolValue = value;
                    OnPropertyChanged("BoolValue");
                }
            }
        }

        private string _textValue;

        public string TextValue {
            get { return _textValue; }
            set
            {
                if (_textValue != value)
                {
                    _textValue = value.ToUpper();
                    OnPropertyChanged("TextValue");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                System.Diagnostics.Debug.WriteLine("Fired");
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
