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
    class CustomListViewAdapter : BaseAdapter<Person>
    {
        List<Person> persons;
        Activity context;

        public CustomListViewAdapter(Activity context, List<Person> persons) : base()
        {
            this.context = context;
            this.persons = persons;
        }

        public override int Count
        {
            get
            {
                return persons.Count;
            }
        }

        public override Person this[int position]
        {
            get
            {
                return persons[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Ç±Ç±Ç… View ÇÃé¿ëïÇèëÇ´Ç‹Ç∑ÅB
            var person = persons[position];

            // ACTIVITY LIST ITEM
            //View view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            //var txt = view.FindViewById<TextView>(Resource.Id.Name);
            //txt.Text = person.Name;
            //txt.TextSize = 40;
            //var img = view.FindViewById<ImageView>(Resource.Id.Icon);
            //img.SetImageResource(person.ImageResourceId);

            //Full CustomView
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            view.FindViewById<ImageView>(Resource.Id.Icon).SetImageResource(person.ImageResourceId);
            view.FindViewById<TextView>(Resource.Id.Name).Text = person.Name;
            view.FindViewById<TextView>(Resource.Id.Description).Text = person.Description;
            

            
            

            return view;
        }
    }
}