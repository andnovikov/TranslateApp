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

namespace TranslateApp.Droid
{
    [Activity(Label = "Add to dictionary")]
    [IntentFilter (new[]{Intent.ActionSend}, 
        Categories=new[]{Intent.CategoryDefault},
        DataMimeType= "text/plain")]

    public class WordAddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WordAdd);

            // Get intent, action and MIME type
            if (this.Intent.Type.Equals("text/plain"))
            {
                handleSendText(this.Intent);
            }
        }

        private void handleSendText(Intent intent)
        {
            // Get the text from intent
            String sharedText = intent.GetStringExtra(Intent.ExtraText);
            // When Text is not null
            if (sharedText != null)
            {
                /*EditText e = new EditText(this);
                e.Text*/
                EditText e = (EditText)FindViewById(Resource.Id.editSourceWord);
                e.Text = sharedText;

                // Show the text as Toast message
                Toast.MakeText(this, sharedText, ToastLength.Long).Show();
            }
        }

        [Java.Interop.Export("OnClickBtnAdd")]
        public void OnClickBtnAdd(View view)
        {
            Database db = new Database();

            EditText eSourceWord = (EditText)FindViewById(Resource.Id.editSourceWord);
            EditText eTranslateWord = (EditText)FindViewById(Resource.Id.editTranslateWord);

            Word word = new Word() { SourceWord = eSourceWord.Text, TranslateWord = eTranslateWord.Text};

            db.insertUpdateWord(word);
        }
    }
}