using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF_MvvmSample.ViewModel
{
    class PageViewModel : INotifyPropertyChanged
    {
        string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        //private bool _toggled;

        //public bool Toggled
        //{
        //    get { return _toggled; }
        //    set
        //    {
        //        if (_toggled != value)
        //        {
        //            _toggled = value;
        //            OnPropertyChanged("Toggled");
        //            //OnPropertyChanged("Message");
        //        }
        //    }
        //}


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
