using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using X_GpsSample;
using Geolocator.Plugin;

namespace XF_GpsSample
{
    public partial class GetGeoXaml : ContentPage
    {
        Position centerPosition = new Position(35.685208d, 139.752799d); // 皇居（中心位置）
        Pin pin;
        GetAddress getAddress = new GetAddress();

        public GetGeoXaml()
        {
            InitializeComponent();

            map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(4d)));

            button.Clicked += async (sender, e) =>
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

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
        }
    }
}
