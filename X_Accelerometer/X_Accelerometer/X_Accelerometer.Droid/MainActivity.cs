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
        float nowX;
        float nowY;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            abs = new AbsoluteLayout(this);

            sensorText = new TextView(this);
            sensorText.SetBackgroundColor(new Color(192, 192, 192));
            sensorText.Gravity = GravityFlags.Center;

            SetContentView(abs);

            sensorManager = (SensorManager)GetSystemService(SensorService);

            //Point prealsize = new Point();
            //WindowManager.DefaultDisplay.GetRealSize(prealsize); // ディスプレイサイズ取得（4.2以降）
            //Log.Debug("Info", string.Format("Width(GetRealSize): {0}, Height(GetRealSize): {1}", prealsize.X ,prealsize.Y);

            //Point psize = new Point();
            //WindowManager.DefaultDisplay.GetSize(psize); // ナビゲーションバー以外のサイズ
            //Log.Debug("Info", string.Format("Width(GetSize): {0}, Height(GetSize): {1}", psize.X, psize.Y);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            //Log.Debug("Info", string.Format("Width(AbsoluteLayout): {0},Height(AbsoluteLayout): {1}", abs.Width, abs.Height));

            // ContentViewに配置したAbsoluteLayoutのサイズ
            layoutW = abs.Width;
            layoutH = abs.Height;

            // TextViewのサイズを指定・配置
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
                // 現在の位置を取得
                nowX = sensorText.GetX();
                nowY = sensorText.GetY();

                Log.Debug("Info", string.Format("nowX: {0}, nowY: {1}", nowX, nowY));

                // Viewをはみ出さないようにAccelerometerの値によってTextViewを移動
                if (nowX - e.Values[0] * 10 > 0 && nowX - e.Values[0] * 10 < layoutW - textW)
                    sensorText.SetX(nowX - e.Values[0] * 10);

                if (nowY + e.Values[1] * 10 > 0 && nowY + e.Values[1] * 10 < layoutH - textH)
                    sensorText.SetY(nowY + e.Values[1] * 10);
            }
        }
    }
}


