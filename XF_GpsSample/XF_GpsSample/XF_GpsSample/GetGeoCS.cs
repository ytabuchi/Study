using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using X_GpsSample;

namespace XF_GpsSample
{
    public class GetGeoCS : ContentPage
    {
        Label LatLabel;
        Label LonLabel;
        Label AddrLabel;
        Map map;
        Position centerPosition = new Position(35.685208d, 139.752799d); // 皇居（中心位置）
        Pin pin;
        GetAddress getAddress = new GetAddress();

        public GetGeoCS()
        {
            map = new Map(
                MapSpan.FromCenterAndRadius(
                    centerPosition, // 初期位置
                    Distance.FromKilometers(4d))) // 4km 圏内(?)
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HasZoomEnabled = true,
                IsShowingUser = true,
            };

            LatLabel = new Label
            {
                Text = "Lat:",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            LonLabel = new Label
            {
                Text = "Lon:",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            AddrLabel = new Label
            {
                Text = "Address:",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            var getGeoButton = new Button
            {
                Text = "Get Location",
            };
            getGeoButton.Clicked += async (sender, e) =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                locator.AllowsBackgroundUpdates = true;

                var location = await locator.GetPositionAsync(10000);

                LatLabel.Text = "Lat: " + location.Latitude.ToString("N6");
                LonLabel.Text = "Lon: " + location.Longitude.ToString("N6");

                var addr = await getAddress.GetJsonAsync(location.Latitude, location.Longitude) ?? "取得できませんでした";

                AddrLabel.Text = "Address: " + addr;

                // Map を移動させてピン打ち
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(4d)));
                if (map.Pins.Count() > 0)
                    map.Pins.Clear();
                pin = new Pin
                {
                    Position = new Position(location.Latitude, location.Longitude),
                    Label = addr,
                };
                map.Pins.Add(pin);
            };

            Title = "XF_GpsSample";
            Content = new StackLayout
            {
                Padding = new Thickness(8),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    getGeoButton,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            LatLabel,
                            LonLabel,
                        }
                    },
                    AddrLabel,
                    map
                }
            };
        }
    }
}
