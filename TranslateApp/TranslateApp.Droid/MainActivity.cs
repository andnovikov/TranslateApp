using System;

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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            Database db = new Database();

            ListView listView = (ListView)this.FindViewById(Resource.Id.listView);
            if (listView != null)
            {
                Toast.MakeText(this, "ListView found.", ToastLength.Long).Show();
            }
        }
    }
}


