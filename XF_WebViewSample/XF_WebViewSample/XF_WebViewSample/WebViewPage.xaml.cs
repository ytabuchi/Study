using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_WebViewSample
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string url)
        {
            InitializeComponent();

            browser.Source = url;
        }

        void BackClicked(object sender, EventArgs s)
        {
            if (browser.CanGoBack)
                browser.GoBack();
        }

        void ForwardClicked(object sender, EventArgs s)
        {
            if (browser.CanGoForward)
                browser.GoForward();
        }

        void GoClicked(object sender, EventArgs s)
        {
            if (urlEntry.Text != null && urlEntry.Text.Contains("http"))
            {
                browser.Source = urlEntry.Text;
            }
        }

        void WebOnNavigating(object sender, WebNavigatingEventArgs s)
        {
            loading.IsVisible = true;
        }

        void WebOnNavigated(object sender, WebNavigationEventArgs s)
        {
            loading.IsVisible = false;
            urlEntry.Text = s.Url;
        }
    }
}
