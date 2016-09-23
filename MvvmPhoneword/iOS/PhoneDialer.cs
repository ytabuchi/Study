using System;
using Foundation;
using UIKit;
using MvvmPhoneword.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace MvvmPhoneword.iOS
{
    public class PhoneDialer : Helpers.IDialer
    {
        public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number));
        }
    }
}

