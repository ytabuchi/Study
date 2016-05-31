using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_TabbedPage
{
    public class TabbedPageCS : TabbedPage
    {
        public TabbedPageCS()
        {
            this.Title = "C# TabbedPage";
            this.Padding = new Thickness(0, Device.OnPlatform(0, 20, 0), 0, 0);

            // ChildrenにPageを列挙していきます。Pageを参照しても直接書いてもOK。
            // iOSにだけIconが必要で、白のアイコンでFILENAME@2xを48px、FILENAME@3xを72pxで用意します。
            this.Children.Add(new Page1()
            {
                Title = "TabPage 1",
                Icon = "ic_one.png",

            });
            this.Children.Add(new ContentPage
            {
                Title = "TabPage 2",
                Icon = "ic_two.png",
                BackgroundColor = Color.FromHex("#eeffee"),
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            Text = "Hello Tabbed Page 2!",
                            TextColor = Color.FromHex("#666666"),
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalTextAlignment = TextAlignment.Center
                        },
                        new Button
                        {
                            Text = "Open Modal",
                            Command = new Command(async () =>
                            {
                                await Navigation.PushModalAsync(new ContentPage
                                {
                                    Content = new Button
                                    {
                                        Text = "Back",
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Command = new Command(async () =>
                                        {
                                            await Navigation.PopModalAsync();
                                        })
                                    }
                                });
                            })
                        }
                    }
                }
            });
            this.Children.Add(new NavigationPage(new Page3())
            {
                Title = "TabPage 3",
                Icon = "ic_three.png"
            });

        }
    }
}
