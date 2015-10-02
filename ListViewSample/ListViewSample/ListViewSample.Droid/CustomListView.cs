using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ListViewSample;
using System.Collections.Generic;

namespace ListViewSample.Droid
{
	[Activity (Label = "ListViewSample.Droid", MainLauncher = true)]
	public class CustomListView : ListActivity
	{
        List<Person> persons = new List<Person>();

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            //SetContentView (Resource.Layout.Main);

            #region //persons に追加
            persons.Add(new Person { Name = "Alen", ImageResourceId = Resource.Drawable.A });
            persons.Add(new Person { Name = "Brawn", ImageResourceId = Resource.Drawable.B });
            persons.Add(new Person { Name = "Charie", ImageResourceId = Resource.Drawable.C });
            persons.Add(new Person { Name = "Danny", ImageResourceId = Resource.Drawable.D });
            persons.Add(new Person { Name = "Eric", ImageResourceId = Resource.Drawable.E });
            persons.Add(new Person { Name = "Alen2", ImageResourceId = Resource.Drawable.A });
            persons.Add(new Person { Name = "Brawn2", ImageResourceId = Resource.Drawable.B });
            persons.Add(new Person { Name = "Charie2", ImageResourceId = Resource.Drawable.C });
            persons.Add(new Person { Name = "Danny2", ImageResourceId = Resource.Drawable.D });
            persons.Add(new Person { Name = "Eric2", ImageResourceId = Resource.Drawable.E });
            persons.Add(new Person { Name = "Alen3", ImageResourceId = Resource.Drawable.A });
            persons.Add(new Person { Name = "Brawn3", ImageResourceId = Resource.Drawable.B });
            persons.Add(new Person { Name = "Charie3", ImageResourceId = Resource.Drawable.C });
            persons.Add(new Person { Name = "Danny3", ImageResourceId = Resource.Drawable.D });
            persons.Add(new Person { Name = "Eric3", ImageResourceId = Resource.Drawable.E });
            persons.Add(new Person { Name = "Alen4", ImageResourceId = Resource.Drawable.A });
            persons.Add(new Person { Name = "Brawn4", ImageResourceId = Resource.Drawable.B });
            persons.Add(new Person { Name = "Charie4", ImageResourceId = Resource.Drawable.C });
            persons.Add(new Person { Name = "Danny4", ImageResourceId = Resource.Drawable.D });
            persons.Add(new Person { Name = "Eric4", ImageResourceId = Resource.Drawable.E });
            persons.Add(new Person { Name = "Alen5", ImageResourceId = Resource.Drawable.A });
            persons.Add(new Person { Name = "Brawn5", ImageResourceId = Resource.Drawable.B });
            persons.Add(new Person { Name = "Charie5", ImageResourceId = Resource.Drawable.C });
            persons.Add(new Person { Name = "Danny5", ImageResourceId = Resource.Drawable.D });
            persons.Add(new Person { Name = "Eric5", ImageResourceId = Resource.Drawable.E });
            #endregion

            ListAdapter = new CustomListViewAdapter(this, persons);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = persons[position];
            Toast.MakeText(this, string.Format("{0} is tapped.", t.Name), ToastLength.Short).Show();
            //var intent = new Intent(this, typeof(ResultActivity));
            
        }
    }
}


