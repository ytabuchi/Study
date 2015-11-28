using System;
using CoreMotion;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace X_Accelerometer.iOS
{
    public partial class ViewController : UIViewController
    {
        CMMotionManager motionManager;
        nfloat layoutW;
        nfloat layoutH;
        nfloat textW;
        nfloat textH;
        nfloat nowX;
        nfloat nowY;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Viewのサイズを取得
            layoutW = View.Bounds.Width;
            layoutH = View.Bounds.Height;

            // Viewサイズを元にLabelサイズを指定
            textW = layoutW / 2;
            textH = layoutH / 6;

            System.Diagnostics.Debug.WriteLine("Width: {0}, Height: {1}", layoutW, layoutH);

            // Label作成＆配置
            var sensorText = new UILabel(new CGRect(layoutW / 2 - textW / 2, layoutH / 2 - textH / 2, textW, textH));
            sensorText.BackgroundColor = UIColor.FromRGB(192, 192, 192);
            sensorText.Lines = 0;
            sensorText.TextAlignment = UITextAlignment.Center;
            sensorText.LineBreakMode = UILineBreakMode.TailTruncation;
            View.AddSubview(sensorText);

            // Frameを取得して変数に
            var textLoc = sensorText.Frame;

            motionManager = new CMMotionManager();

            if (motionManager.AccelerometerAvailable)
            {
                // Accelerometer Update間隔
                motionManager.AccelerometerUpdateInterval = 0.015;
                // UpdateがQueueに入る度に処理を行う（多分）
                motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    sensorText.Text = string.Format("X = {0:N4}\nY = {1:N4}", data.Acceleration.X, data.Acceleration.Y);
                    // 現在の位置を取得
                    nowX = sensorText.Frame.X;
                    nowY = sensorText.Frame.Y;

                    System.Diagnostics.Debug.WriteLine("nowX: {0}, nowY: {1}", nowX, nowY);

                    // Viewをはみ出さないようにAccelerometerの値によってLabelを移動
                    if (nowX + (nfloat)data.Acceleration.X * 10 > 0 && nowX + (nfloat)data.Acceleration.X * 10 < layoutW - textW)
                        textLoc.X = nowX + (nfloat)data.Acceleration.X * 10;

                    if (nowY - (nfloat)data.Acceleration.Y * 10 > 0 && nowY - (nfloat)data.Acceleration.Y * 10 < layoutH - textH)
                        textLoc.Y = nowY - (nfloat)data.Acceleration.Y * 10;

                    sensorText.Frame = textLoc;
                });
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

