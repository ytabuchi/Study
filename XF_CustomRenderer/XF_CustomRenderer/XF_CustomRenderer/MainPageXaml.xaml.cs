using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_CustomRenderer
{
    public partial class MainPageXaml : ContentPage
    {
        public MainPageXaml()
        {
            InitializeComponent();
        }

        async void ButtonClicked(object sender, EventArgs s)
        {
            await DisplayAlert("Tapped", string.Format($"{((Button)sender).Text} is tapped."), "OK");
            //throw new NotImplementedException();
        }

    }
}
