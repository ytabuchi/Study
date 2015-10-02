using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ListViewSample.Droid
{
    [Activity(Label = "ResultView")]
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Result);

            var image = FindViewById<ImageView>(Resource.Id.Icon);
            var name = FindViewById<TextView>(Resource.Id.Description);

            
        
        }
    }
}