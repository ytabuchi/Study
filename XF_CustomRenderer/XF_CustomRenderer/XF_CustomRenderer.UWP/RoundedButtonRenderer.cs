using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XF_CustomRenderer;
using XF_CustomRenderer.UWP;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace XF_CustomRenderer.UWP
{
    class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var customStyle = App.Current.Resources["RoundedButtonStyle"] as Windows.UI.Xaml.Style;
            if (Control != null)
            {
                Control.Style = customStyle;
            }
        }
    }
}
