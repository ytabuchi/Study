using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using Android.Util;
using Android.Graphics;

namespace X_Accelerometer.Droid
{
	[Activity (Label = "X_Accelerometer.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity // , ISensorEventListener
    {
        static readonly object syncLock = new object();
        SensorManager sensorManager;
        TextView sensorText;
        AbsoluteLayout abs;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            abs = new AbsoluteLayout(this);
            sensorText = new TextView(this);

            // Set our view from the "main" layout resource
            SetContentView(abs);

            sensorManager = (SensorManager)GetSystemService(SensorService);
            //sensorText = FindViewById<TextView>(Resource.Id.SensorText);

            Point psize = new Point();
            Point prealsize = new Point();
            WindowManager.DefaultDisplay.GetSize(psize);
            WindowManager.DefaultDisplay.GetRealSize(prealsize);

            Log.Debug("Info", "Width(GetSize): " + psize.X);
            Log.Debug("Info", "Height(GetSize): " + psize.Y);
            Log.Debug("Info", "Width(GetRealSize): " + prealsize.X);
            Log.Debug("Info", "Height(GetRealSize): " + prealsize.Y);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            Log.Debug("Info", "Width(AbsoluteLayout): " + abs.Width);
            Log.Debug("Info", "Height(AbsoluteLayout): " + abs.Height);

            int textWidth = 400;

            abs.AddView(sensorText, new AbsoluteLayout.LayoutParams(400, 150, 10, 10));

        }

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    sensorManager.UnregisterListener(this);
        //}

        //public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        //{
        //    //throw new NotImplementedException();
        //    //Log.Debug("OnAccuracyChanged", accuracy.ToString());
        //}

        //public void OnSensorChanged(SensorEvent e)
        //{
        //    lock (syncLock)
        //    {
        //        var text = string.Format("X = {0:00.00000}\nY = {1:00.00000}\nZ = {2:00.00000}", e.Values[0], e.Values[1], e.Values[2]);
        //        sensorText.Text = text;
        //    }
        //}
    }
}


