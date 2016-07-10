using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MvvmPhoneword.Models;

namespace MvvmPhoneword.ViewModels
{
    public class CallHistoryPageViewModel : INotifyPropertyChanged
    {
        private List<NumberData> phoneNumbers;
        public List<NumberData> PhoneNumbers
        {
            get { return phoneNumbers; }
            set
            {
                if (phoneNumbers != value)
                {
                    phoneNumbers = value;
                    OnPropertyChanged();
                }
            }
        }

        public CallHistoryPageViewModel()
        {
            this.PhoneNumbers = Numbers.Instance.PhoneNumbers;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

