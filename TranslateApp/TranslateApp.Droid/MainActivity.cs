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
using TranslateApp.Droid.Adapters;

namespace TranslateApp.Droid
{
	[Activity (Label = "TranslateApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

        List<Word> tableItems = new List<Word>();
        ListView listView;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            Database db = new Database();
            db.DBInit();

            listView = FindViewById<ListView>(Resource.Id.WordList);
            List<Word> words = db.getAllWordsSync();

            listView.Adapter = new WordListAdapter(this, words);
        }
    }
}


