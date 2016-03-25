using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF_CustomRenderer;
using XF_CustomRenderer.iOS;

[assembly: ExportRenderer(typeof(RoundedButton),typeof(RoundedButtonRenderer))]
namespace XF_CustomRenderer.iOS
{
    class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var c = UIColor.FromRGB(0.867f, 1.0f, 0.867f); // #ddffdd
                Control.Layer.CornerRadius = 20f;
                Control.Layer.BackgroundColor = c.CGColor;
            }
        }
    }
}
