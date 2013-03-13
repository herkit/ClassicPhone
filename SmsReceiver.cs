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
    [BroadcastReceiver]
    public class SmsReceiver : BroadcastReceiver
    {
        public const string RINGTONE_PORT = "5505";
        public const string OPERATOR_LOGO_PORT = "5506";
        public const string CALLER_GROUP_GRAPHIC_PORT = "5507";

        public override void OnReceive(Context context, Intent intent)
        {
            

            if (intent.Action == "android.intent.action.DATA_SMS_RECEIVED")
                Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
        }
    }
}