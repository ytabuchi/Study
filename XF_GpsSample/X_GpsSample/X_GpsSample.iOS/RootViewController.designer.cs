// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace X_GpsSample.iOS
{
	[Register ("RootViewController")]
	partial class RootViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel AddrText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LatitudeText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LongitudeText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView map { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AddrText != null) {
				AddrText.Dispose ();
				AddrText = null;
			}
			if (button != null) {
				button.Dispose ();
				button = null;
			}
			if (LatitudeText != null) {
				LatitudeText.Dispose ();
				LatitudeText = null;
			}
			if (LongitudeText != null) {
				LongitudeText.Dispose ();
				LongitudeText = null;
			}
			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
