using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SendBinary
{
    [Activity(Label = "SendBinary", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            Button button2 = FindViewById<Button>(Resource.Id.MyButton2);

            button.Click += delegate {
                var sm = Android.Telephony.SmsManager.Default;
                sm.SendDataMessage("5554", null, 5506, new byte[] { 0x00, 0x04, 0x04, 0x01, 0xFF, 0xFF }, null, null);
            };

            button2.Click += delegate
            {
                var sm = Android.Telephony.SmsManager.Default;
                sm.SendTextMessage("5554", null, "Testmelding fra emulator", null, null);
            };
        }
    }
}

