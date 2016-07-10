using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MvvmPhoneword.Models
{
    public class Numbers : INotifyPropertyChanged
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static Numbers Instance { get; } = new Numbers();

        /// <summary>
        /// Private Constructor for showing just one instance.
        /// </summary>
        private Numbers()
        {            
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged();
                    OnPropertyChanged("TranslatedNumber");
                }
            }
        }

        public string TranslatedNumber
        {
            get { return ToNumber(PhoneNumber); }
        }

        private List<NumberData> phoneNumbers = new List<NumberData>();
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

        public void Dial()
        {
            // Dialする際にListに追加して置き換えます。
            var newNumbers = this.PhoneNumbers;
            newNumbers.Add(new NumberData(PhoneNumber, TranslatedNumber, DateTime.Now));
            PhoneNumbers = newNumbers;

            // DependencyServiceでネイティブのDialメソッドを呼び出します。
            var dialer = DependencyService.Get<Helpers.IDialer>();
            dialer?.Dial(TranslatedNumber);

        }


        #region ToNumber
        /// <summary>
        /// Strings to phone number method
        /// </summary>
        /// <returns>Translated phone number</returns>
        /// <param name="raw">Raw phone strings</param>
        public string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;

            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if (" -0123456789".Contains(c.ToString()))
                    newNumber.Append(c);
                else
                {
                    var result = TranslateToNumber(c);
                    if (result != null)
                        newNumber.Append(result);
                    // Bad character?
                    else
                        return null;
                }
            }
            return newNumber.ToString();
        }

        private readonly string[] digits = {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        private int? TranslateToNumber(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c.ToString()))
                    return 2 + i;
            }
            return null;
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

