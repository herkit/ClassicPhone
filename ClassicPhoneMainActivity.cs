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

namespace ClassicPhone
{
    [Activity(Label = "Classic Phone", MainLauncher=true)]
    public class ClassicPhoneMainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var filter = new IntentFilter("android.intent.action.DATA_SMS_RECEIVED");
            filter.AddDataScheme("sms");
            filter.AddDataAuthority("*", "5606");

            var receiver = new SmsReceiver();
            RegisterReceiver(receiver, filter);
            // Create your application here
        }
    }
}