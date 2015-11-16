using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreLocation;
using MapKit;
using X_GpsSample;

namespace X_GpsSample.iOS
{
    public partial class RootViewController : UIViewController
    {
        CLLocationManager locMgr = null;
        CLLocationCoordinate2D centerPosition = new CLLocationCoordinate2D(35.685344d, 139.753029d); // 皇居（中心位置）
        GetAddress getAddress = new GetAddress();

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public RootViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            map.SetRegion(
                new MKCoordinateRegion(
                    centerPosition, // 初期位置 
                    new MKCoordinateSpan(0.1d, 0.1d)),
                true);

            locMgr = new CLLocationManager();
            locMgr.RequestWhenInUseAuthorization();

            button.TouchUpInside += (s, _) =>
            {
                locMgr.DesiredAccuracy = 1000;
                locMgr.LocationsUpdated += async (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    var location = e.Locations[e.Locations.Length - 1];
                    LatitudeText.Text = "Lat: " + location.Coordinate.Latitude.ToString("N6");
                    LongitudeText.Text = "Lon: " + location.Coordinate.Longitude.ToString("N6");

                    // PCL で Google Maps API Web サービスに Lat, Lon を投げて住所を取得しています。
                    var addr = await getAddress.GetJsonAsync(location.Coordinate.Latitude, location.Coordinate.Longitude) ?? "取得できませんでした";
                    System.Diagnostics.Debug.WriteLine("AddressResult", addr);
                    AddrText.Text = "Address: " + addr;

                    // Map 移動
                    map.SetRegion(
                        new MKCoordinateRegion(
                            new CLLocationCoordinate2D(location.Coordinate.Latitude, location.Coordinate.Longitude),
                            new MKCoordinateSpan(0.1d, 0.1d)),
                        true);
                    // Pin 追加
                    map.AddAnnotations(new MKPointAnnotation()
                    {
                        Title = addr,
                        Coordinate = new CLLocationCoordinate2D(location.Coordinate.Latitude, location.Coordinate.Longitude)
                    });

                    locMgr.StopUpdatingLocation();
                };
                locMgr.StartUpdatingLocation(); // なぜ下に書くのかあめいさんに聞いてみよう。


            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}