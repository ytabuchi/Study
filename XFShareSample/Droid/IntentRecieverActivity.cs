using System;
using System.IO;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace XFShareSample.Droid
{
    [Activity(Label = "XFShareSampleで共有")]
    [IntentFilter(new[] { Intent.ActionSend },
                  Categories = new[] { Intent.CategoryDefault },
                  DataMimeType = "text/plain")]
    [IntentFilter(new[] { Intent.ActionSend },
                  Categories = new[] { Intent.CategoryDefault },
                  DataMimeType = "image/*")]
    
    public class IntentRecieverActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.Action == Intent.ActionSend)
            {
                if (Intent.Type.Contains("text/"))
                {
                    //テキスト処理
                    foreach (var key in Intent.Extras.KeySet())
                    {
                        System.Diagnostics.Debug.WriteLine($"【KEY】{key}\n【VALUE】{Intent.Extras.Get(key)}");
                    }
                    Toast.MakeText(this, Intent.Extras.Get(Intent.ExtraText).ToString(), ToastLength.Short).Show();
                }
                else if (Intent.Type.Contains("image/"))
                {
                    //画像処理
                    var stream = Intent.Extras.Get(Intent.ExtraStream);
                    System.Diagnostics.Debug.WriteLine(stream);
                    Toast.MakeText(this, stream.ToString(), ToastLength.Long).Show();
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }
    }
}
