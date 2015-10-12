using Geolocator.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X_GpsSample;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XF_GpsSample
{
    public class GetGeoCS : ContentPage
    {
        Label LatLabel;
        Label LonLabel;
        Label AddrLabel;
        Map map;
        Position centerPosition = new Position(35.171845d, 136.881494d); // 名古屋駅（中心位置）
        Pin pin;
        GetAddress getAddress = new GetAddress();

        public GetGeoCS()
        {

            map = new Map(
                MapSpan.FromCenterAndRadius(
                    centerPosition, // 名古屋駅（中心位置）
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

                var location = await locator.GetPositionAsync(10000);

                LatLabel.Text = "Lat: " + location.Latitude.ToString("N4");
                LonLabel.Text = "Lon: " + location.Longitude.ToString("N4");

                var addr = await getAddress.GetJsonAsync(location.Latitude, location.Longitude) ?? "取得できませんでした";

                AddrLabel.Text = "Address: " + addr;

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position( location.Latitude, location.Longitude), Distance.FromKilometers(4d)));
                if (map.Pins.Count() > 0)
                    map.Pins.Clear();
                pin = new Pin
                {
                    Position = new Position(location.Latitude, location.Longitude),
                    Label = addr,
                };
                map.Pins.Add(pin);
            };

            Title = "XF_GpsSample.Droid";
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
