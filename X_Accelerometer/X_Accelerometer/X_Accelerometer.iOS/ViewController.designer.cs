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

namespace X_Accelerometer.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel sensorLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (sensorLabel != null) {
				sensorLabel.Dispose ();
				sensorLabel = null;
			}
		}
	}
}
