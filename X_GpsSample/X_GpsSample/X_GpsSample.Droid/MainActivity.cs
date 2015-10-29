using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;

using X_GpsSample;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace X_GpsSample.Droid
{
    [Activity(Label = "X_GpsSample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        string tag = "MainActivity";
        Button button;
        TextView latitude;
        TextView longitude;
        TextView provider;
        TextView address;
        GoogleMap map;
        Marker marker;
        GetAddress getAddress = new GetAddress();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            button = FindViewById<Button>(Resource.Id.GetLocationButton);
            latitude = FindViewById<TextView>(Resource.Id.LatitudeText);
            longitude = FindViewById<TextView>(Resource.Id.LongitudeText);
            provider = FindViewById<TextView>(Resource.Id.ProviderText);
            address = FindViewById<TextView>(Resource.Id.AddressText);

            // MapFragment の用意と初期の場所を指定します。
            // MapFragment.Map が deprecated みたいなので、正しい書き方を教えてください＞＜
            MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapFragment);
            map = mapFrag.Map;
            if (map != null) // Map の準備が出来たら (最初は null を返します)
            {
                map.MyLocationEnabled = true; // 現在地ボタン オン
                map.UiSettings.ZoomControlsEnabled = true; // ズームコントロール オン
                // 初期位置(カメラ)を移動
                map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
                    new CameraPosition(
                        new LatLng(35.685344d, 139.753029d), // 皇居（中心位置）
                        12f, 0f, 0f))); // ズームレベル、方位、傾き
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            locMgr = GetSystemService(Context.LocationService) as LocationManager;

#if DEBUG
            // 利用可能な LocationManager の Provider とオンオフをチェック
            foreach (var item in locMgr.AllProviders)
            {
                Log.Debug("NetworkList", "Provider: " + item.ToString());
                Log.Debug("NetworkList", "Avalable: " + locMgr.IsProviderEnabled(item.ToString()));
            }
#endif

            button.Click += (sender, e) =>
            {
                if (button.Text.ToUpper() == "GET LOCATION")
                {
                    button.Text = "Location Service Running";

                    if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider)
                        && locMgr.IsProviderEnabled(LocationManager.NetworkProvider))
                    {
                        // Network: Wifi と 3G で位置情報を取得します。電池使用量は少ないですが精度にばらつきがあります。
                        Log.Debug(tag, "Starting location updates with Network");
                        locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 10000, 10, this);
                    }
                    else
                    {
                        // GetBestProvider で最適な Provider を利用できるようです。(Network があればそれが Best になるようです。)
                        var locationCriteria = new Criteria();
                        locationCriteria.Accuracy = Accuracy.Coarse;
                        locationCriteria.PowerRequirement = Power.Medium;
                        string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

                        Log.Debug(tag, "Starting location updates with " + locationProvider.ToString());
                        locMgr.RequestLocationUpdates(locationProvider, 10000, 10, this);
                    }
                }
                else
                {
                    Log.Debug(tag, "Stop locMgr manually");
                    locMgr.RemoveUpdates(this);
                    button.Text = "Get Location";
                }

            };
        }

        
        public async void OnLocationChanged(Android.Locations.Location location)
        {
            marker = null;
            Log.Debug(tag, "Location changed");
            latitude.Text = "Lat: " + location.Latitude.ToString();
            longitude.Text = "Lon: " + location.Longitude.ToString();
            provider.Text = "Provider: " + location.Provider.ToString();

            // PCL で Google Geocoding API Web サービスに Lat, Lon を投げて住所を取得しています。
            var addr = await getAddress.GetJsonAsync(location.Latitude, location.Longitude) ?? "取得できませんでした";
            Log.Debug("AddressResult", addr);
            address.Text = "Address: " + addr;

            // 取得した Lat, Lon を Map に投げます。
            map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
                    new CameraPosition(
                        new LatLng(location.Latitude, location.Longitude), 14f, 0f, 0f)));
            marker = map.AddMarker(new MarkerOptions()
                .SetTitle(addr)
                .SetPosition(new LatLng(location.Latitude, location.Longitude)));
        }

        public void OnProviderDisabled(string provider)
        {
            // Android 側で手動で位置情報モードを変更すると発火します。
            Log.Debug(tag, provider + " provider disabled");
            locMgr.RemoveUpdates(this);
            button.Text = "Get Location";
            // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            Log.Debug(tag, "Status Changed");
            throw new NotImplementedException();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Log.Debug(tag, "OnPause, stop location manager update");
            locMgr.RemoveUpdates(this);
            button.Text = "Get Location";
        }
    }
}


