using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TranslateApp.DB;

namespace TranslateApp.Droid
{
	[Activity (Label = "TranslateApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        string[] items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            Database db = new Database();
            db.DBInit();

            Task<List<Word>> words = db.getAllWords();
            foreach (Word word in words.Result)
            {
                TextView textWord = new TextView(this) { Text = word.SourceWord};
                var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                    ViewGroup.LayoutParams.WrapContent) { LeftMargin = 20 };
                textWord.LayoutParameters = layoutParams;

                LinearLayout linearLayout = (LinearLayout)FindViewById(Resource.Id.linearLayout);
                linearLayout.AddView(textWord);
            }

            /*
            ListView listView = (ListView)this.FindViewById(Resource.Id.listView);
            if (listView != null)
            {
                Toast.MakeText(this, "ListView found.", ToastLength.Long).Show();
                

                items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
                listView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
                // listView.Adapter = new ArrayAdapter<Word>(this, Resource.Id.listView, words);
            }
            */
        }
    }
}


