using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_QuickStartScreen
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new StartPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class QuickStartLayerVisblity
    {
        public static bool qslVisible { get; set; }
    }

    public class StartPage : ContentPage
    {
        StackLayout ml;
        ContentView qsl;
        ContentView qslwp;
        ContentView bl;
        bool qslVisible = true;

        public StartPage()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "検索",
                Icon = "Search.png",
                Command = new Command(() => DisplayAlert("Search", "Search is tapped.", "OK")),
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "設定",
                Icon = "Setting.png",
                Command = new Command(() => DisplayAlert("Setting", "Setting is tapped.", "OK")),
            });

            AbsoluteLayout abs = new AbsoluteLayout { };

            ml = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = 15,
                Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Welcome to Xamarin Forms!",
                        TextColor = Color.Black,
					},
                    new Button {
                        Text = "Show QuickStart",
                        TextColor = Color.Black,
                        BackgroundColor = Color.FromHex("CCC"),
                        BorderColor = Color.Gray,
                        BorderRadius = 5,
                        BorderWidth = 1,
                        Command = new Command(()=>{
                            qslVisible = true;
                            Application.Current.Properties["qsl"] = qslVisible;
                            DisplayAlert("Show QuickStart", "Show QuickStart when you re-launch this app.", "OK");
                        }),
                    },
				},
            };

            qsl = new ContentView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 0, 10, 10),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new Image {
                            Source = "QuickStart.png",
                            WidthRequest = 250,
                            HorizontalOptions = LayoutOptions.End,
                        },
                        new Image {
                            Source = "QuickStartSwipe.png",
                            WidthRequest = 340,
                        },
                        new Button {
                            Text = "閉じる",
                            TextColor = Color.White,
                            BackgroundColor = Color.FromHex("49d849"),
                            BorderRadius = 5,
                            VerticalOptions = LayoutOptions.EndAndExpand,
                            Command = new Command (()=>{
                                qsl.IsVisible = false;
                                bl.IsVisible = false;
                                qslVisible = false;
                                Application.Current.Properties["qsl"] = qslVisible;
                            }),
                        },
                    },
                },
            };

            qslwp = new ContentView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 70, 10, 0),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new Image {
                            Source = "QuickStartSwipe.png",
                            WidthRequest = 460,
                        },
                        new Button {
                            Text = "閉じる",
                            TextColor = Color.White,
                            BackgroundColor = Color.FromHex("49d849"),
                            BorderRadius = 5,
                            Command = new Command (()=>{
                                qslwp.IsVisible = false;
                                bl.IsVisible = false;
                                qslVisible = false;
                                Application.Current.Properties["qsl"] = qslVisible;
                            }),
                        },
                        new Image {
                            Source = "QuickStart.png",
                            WidthRequest = 460,
                        },
                    },
                },
            };

            bl = new ContentView
            {
                BackgroundColor = Color.Black,
                Opacity = 0.65d,
            };

            abs.Children.Add(ml);
            if (Application.Current.Properties.ContainsKey("qsl"))
            {
                var bqsl = (bool)Application.Current.Properties["qsl"];
                if (bqsl)
                {
                    abs.Children.Add(bl);
                    if (Device.OS == TargetPlatform.WinPhone)
                    {
                        abs.Children.Add(qslwp);
                    }
                    else
                    {
                        abs.Children.Add(qsl);
                    }
                    
                }
            }
            else
            {
                abs.Children.Add(bl);
                if (Device.OS == TargetPlatform.WinPhone)
                {
                    abs.Children.Add(qslwp);
                }
                else
                {
                    abs.Children.Add(qsl);
                }
            }
            

            Title = "QuickStartLayer";
            Content = abs;

            SizeChanged += OnPageSizeChanged;
        }

        /// <summary>
        /// 画面サイズ変更時に呼び出されます。各コントロールの場所を指定します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            var w = this.Width;
            var h = this.Height;

            AbsoluteLayout.SetLayoutFlags(ml, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(ml, new Rectangle(0d, 0d, w, h));

            AbsoluteLayout.SetLayoutFlags(bl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(bl, new Rectangle(0d, 0d, w, h));

            AbsoluteLayout.SetLayoutFlags(qsl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(qsl, new Rectangle(0d, 0d, w, h));
        }
    }


}
