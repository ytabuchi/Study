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
        nfloat nowX;
        nfloat nowY;
        nfloat layoutW;
        nfloat layoutH;
        nfloat textW;
        nfloat textH;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            layoutW = View.Bounds.Width;
            layoutH = View.Bounds.Height;

            textW = layoutW / 2;
            textH = layoutH / 6;

            var sensorText = new UILabel(new CGRect(layoutW / 2 - textW / 2, layoutH / 2 - textH / 2, textW, textH));
            sensorText.BackgroundColor = UIColor.FromRGB(192, 192, 192);
            sensorText.Lines = 0;
            //sensorText.AdjustsFontSizeToFitWidth = false;
            View.AddSubview(sensorText);

            System.Diagnostics.Debug.WriteLine("X: {0}, Y: {1}", View.Bounds.Width, View.Bounds.Height);

            var textLoc = sensorText.Frame;

            motionManager = new CMMotionManager();

            if (motionManager.AccelerometerAvailable)
            {
                motionManager.AccelerometerUpdateInterval = 0.02;
                motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    sensorText.Text = string.Format("X = {0:N4}\n Y = {1:N4}", data.Acceleration.X, data.Acceleration.Y);

                    nowX = sensorText.Frame.X;
                    nowY = sensorText.Frame.Y;
                    System.Diagnostics.Debug.WriteLine("X: {0}, Y: {1}", nowX, nowY);

                    textLoc.X = nowX + (nfloat)data.Acceleration.X * 10;
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

