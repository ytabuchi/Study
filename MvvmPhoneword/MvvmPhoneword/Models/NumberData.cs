using System;

namespace MvvmPhoneword.Models
{
    public class NumberData
    {
        public string RawNumber { get; }
        public string TranslatedNumber { get; }
        public DateTime CalledDate { get; }

        public NumberData(string rawNumber, string translatedNumber, DateTime calledDate)
        {
            this.RawNumber = rawNumber;
            this.TranslatedNumber = translatedNumber;
            this.CalledDate = calledDate;
        }
    }
}

