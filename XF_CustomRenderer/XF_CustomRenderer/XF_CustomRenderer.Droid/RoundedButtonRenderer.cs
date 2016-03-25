using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF_CustomRenderer;
using XF_CustomRenderer.Droid;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace XF_CustomRenderer.Droid
{
    class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.RoundedButton);
            }
        }
    }
}