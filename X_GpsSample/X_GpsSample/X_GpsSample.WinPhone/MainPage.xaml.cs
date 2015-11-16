using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using X_GpsSample;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace X_GpsSample.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GetAddress getAddress = new GetAddress();
        //Geopoint centerPosition;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //centerPosition = new Geopoint(new BasicGeoposition(new Geoposition(35.685344d, 139.753029d)));
            this.map.Style = MapStyle.None;

            var mapTile = new MapTileSource
            {
                DataSource =
                new HttpMapTileDataSource("http://ecn.t1.tiles.virtualearth.net/tiles/r{quadkey}.png?g=1&mkt=ja-jp")
            };
            this.map.TileSources.Insert(0, mapTile);
        }

        private async void GetLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition location = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                Geocoordinate cordinate = location.Coordinate;

                LatitudeText.Text = "Lat: " + location.Coordinate.Point.Position.Latitude.ToString("N6");
                LongitudeText.Text = "Lon: " + location.Coordinate.Point.Position.Longitude.ToString("N6");

                // PCL で Google Maps API Web サービスに Lat, Lon を投げて住所を取得しています。
                var addr = await getAddress.GetJsonAsync(location.Coordinate.Point.Position.Latitude, location.Coordinate.Point.Position.Longitude) ?? "取得できませんでした";
                System.Diagnostics.Debug.WriteLine("AddressResult", addr);
                AddressText.Text = "Address: " + addr;

                this.map.Center = cordinate.Point;
                map.ZoomLevel = 15;

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    AddressText.Text = "location  is disabled in phone settings.";
                }
                //else
                {
                    // something else happened acquring the location
                }
            }
        }
    }
}
