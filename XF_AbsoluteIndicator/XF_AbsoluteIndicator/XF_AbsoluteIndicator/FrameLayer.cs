using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AbsoluteIndicator
{
    public class FrameLayer : Frame
    {
        public FrameLayer()
        {
            BackgroundColor = Color.Black.MultiplyAlpha(0.8d);
            Content = new StackLayout
            {
                Children =
                {
                    new ActivityIndicator { IsRunning = true },
                    new Label { Text = "Data loading...", TextColor = Color.White, },
                }
            };
            IsVisible = false;
        }
    }
}
