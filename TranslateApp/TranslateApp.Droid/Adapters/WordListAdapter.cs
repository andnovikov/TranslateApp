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

using TranslateApp.DB;

namespace TranslateApp.Droid.Adapters
{
    public class WordListAdapter : BaseAdapter<Word>
    {
        List<Word> items;
        Activity context;

        public WordListAdapter(Activity context, List<Word> items): base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Word this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomWordLayout, null);
            view.FindViewById<TextView>(Resource.Id.SourceWord).Text = item.SourceWord;
            view.FindViewById<TextView>(Resource.Id.TranslateWord).Text = item.TranslateWord;

            return view;
        }
    }
}