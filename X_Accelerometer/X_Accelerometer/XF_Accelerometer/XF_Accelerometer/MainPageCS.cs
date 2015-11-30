using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace XF_Accelerometer
{
    public class MainPageCS : ContentPage
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
        AbsoluteLayout abs;
        Label SensorText;

        public MainPageCS()
        {
            abs = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            SensorText = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Silver,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "test"
            };

            abs.Children.Add(SensorText);

            Content = abs;

            SizeChanged += MainPageCS_SizeChanged;

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
                        if (nowX + moveX > ((layoutW - textW) / 2) * -1 && nowX + moveX < (layoutW - textW) / 2)
                        {
                            SensorText.TranslationX = nowX + moveX;
                            nowX = nowX + moveX;
                        }
                        if (nowY + moveY > ((layoutH - textH) / 2) * -1 && nowY + moveY < (layoutH - textH) / 2)
                        {
                            SensorText.TranslationY = nowY + moveY;
                            nowY = nowY + moveY;
                        }
                    });
                };
            }
        }

        private void MainPageCS_SizeChanged(object sender, EventArgs e)
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
