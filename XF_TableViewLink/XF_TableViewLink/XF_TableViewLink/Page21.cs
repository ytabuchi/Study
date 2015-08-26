using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_TableViewLink
{
    public class Page21 : ContentPage
    {
        public Page21()
        {
            Title = "Page 2-1";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        Text = Title,
                        XAlign = TextAlignment.Center,
                    },
                },
            };
        }
    }
}
