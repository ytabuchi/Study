using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_WebViewSample
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async void OnButtonClick(object sender, EventArgs s)
        {
            if (entry.Text != null)
            await Navigation.PushAsync(new WebViewPage(entry.Text));
        }
    }
}
