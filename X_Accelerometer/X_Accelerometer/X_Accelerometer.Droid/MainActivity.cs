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
    [Activity(Label = "X_Accelerometer.Droid", MainLauncher = true)]
    public class MainActivity : Activity, ISensorEventListener
    {
        static readonly object syncLock = new object();
        SensorManager sensorManager;
        TextView sensorText;
        AbsoluteLayout abs;
        int layoutW;
        int layoutH;
        int textW;
        int textH;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            abs = new AbsoluteLayout(this);

            sensorText = new TextView(this);
            sensorText.SetBackgroundColor(new Color(192, 192, 192));
            sensorText.Gravity = GravityFlags.Center;

            SetContentView(abs);

            sensorManager = (SensorManager)GetSystemService(SensorService);

            Point prealsize = new Point();
            WindowManager.DefaultDisplay.GetRealSize(prealsize); // ディスプレイサイズ取得（4.2以降）
            Log.Debug("Info", "Width(GetRealSize): " + prealsize.X);
            Log.Debug("Info", "Height(GetRealSize): " + prealsize.Y);

            Point psize = new Point();
            WindowManager.DefaultDisplay.GetSize(psize); // ナビゲーションバー以外のサイズ
            Log.Debug("Info", "Width(GetSize): " + psize.X);
            Log.Debug("Info", "Height(GetSize): " + psize.Y);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            // ContentViewに配置したAbsoluteLayoutのサイズ
            Log.Debug("Info", "Width(AbsoluteLayout): " + abs.Width);
            Log.Debug("Info", "Height(AbsoluteLayout): " + abs.Height);

            layoutW = abs.Width;
            layoutH = abs.Height;

            textW = layoutW / 2;
            textH = layoutH / 5;

            if (!sensorText.IsShown)
                abs.AddView(sensorText, new AbsoluteLayout.LayoutParams(textW, textH, layoutW / 2 - textW / 2, layoutH / 2 - textH / 2));
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (syncLock)
            {
                sensorText.Text = string.Format("X = {0:N4}\nY = {1:N4}", e.Values[0], e.Values[1]);

                var nowX = sensorText.GetX();
                var nowY = sensorText.GetY();

                Log.Debug("Info", string.Format("nowX: {0}, nowY: {1}", nowX, nowY));

                if (nowX - e.Values[0] * 10 > 0 && nowX - e.Values[0] * 10 < layoutW - textW)
                    sensorText.SetX(nowX - e.Values[0] * 10);

                if (nowY + e.Values[1] * 10 > 0 && nowY + e.Values[1] * 10 < layoutH - textH)
                    sensorText.SetY(nowY + e.Values[1] * 10);
            }
        }
    }
}


