using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_Accelerometer
{
    public partial class MainPageXaml : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;
        double layoutW;
        double layoutH;
        double textW;
        double textH;
        double nowX;
        double nowY;
        double moveX;
        double moveY;

        public MainPageXaml()
        {
            InitializeComponent();

            SizeChanged += MainPageXaml_SizeChanged; //Viewのサイズが決まった時のイベント

            // Motion処理
            motion.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Ui);
            if (motion.IsActive(MotionSensorType.Accelerometer))
            {
                // 現在の位置を取得
                nowX = SensorText.X;
                nowY = SensorText.Y;

                motion.SensorValueChanged += (object sender, SensorValueChangedEventArgs e) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SensorText.Text = string.Format("X: {0:N4}\nY: {1:N4}", ((MotionVector)e.Value).X, ((MotionVector)e.Value).Y);
                    });

                    if (Device.OS == TargetPlatform.Android)
                    {
                        moveX = ((MotionVector)e.Value).X * -10;
                        moveY = ((MotionVector)e.Value).Y * 10;
                    }
                    else
                    {
                        moveX = ((MotionVector)e.Value).X * 10;
                        moveY = ((MotionVector)e.Value).Y * -10;
                    }

                    //Debug.WriteLine($"layoutW: {layoutW}, layoutH: {layoutH}");
                    //Debug.WriteLine($"nowX: {nowX}, nowY: {nowY}");
                    //Debug.WriteLine($"moveX: {moveX}, moveY: {moveY}");

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (nowX + moveX > ((layoutW - textW) / 2) * -1 && nowX + moveX < (layoutW - textW) / 2 && nowY + moveY > ((layoutH - textH) / 2) * -1 && nowY + moveY < (layoutH - textH) / 2)
                        {
                            SensorText.TranslateTo(nowX + moveX, nowY + moveY);

                            nowX = nowX + moveX;
                            nowY = nowY + moveY;
                        }
                    });
                };
            }
        }

        /// <summary>
        /// SizeChangedで呼び出されるメソッドを作成します。AbsoluteLayoutはここで位置を決めます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPageXaml_SizeChanged(object sender, EventArgs e)
        {
            layoutW = abs.Width;
            layoutH = abs.Height;
            textW = layoutW / 2;
            textH = layoutH / 6;

            Debug.WriteLine($"Log: (Size Changed)abs width and height: {layoutW} * {layoutH}");
            Debug.WriteLine($"Log: SensorText size: {textW} * {textH}");

            AbsoluteLayout.SetLayoutFlags(SensorText, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SensorText, new Rectangle(0.5d, 0.5d, textW, textH));
        }
    }
}
