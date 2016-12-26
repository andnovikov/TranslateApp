using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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

            // Get intent, action and MIME type
            Intent intent = new Intent();
            String action = intent.Action;
            String type = intent.Type;
        }

        private void handleSendText(Intent intent)
        {
            // Get the text from intent
            String sharedText = intent.GetStringExtra(Intent.ExtraText);
            // When Text is not null
            if (sharedText != null)
            {
                // Show the text as Toast message
                Toast.MakeText(this, sharedText, ToastLength.Long).Show();
            }
        }
    }
}


