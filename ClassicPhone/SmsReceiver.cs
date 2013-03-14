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
using Android.Telephony;
using Android.Util;

namespace ClassicPhone
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.intent.action.DATA_SMS_RECEIVED" }, Priority = 2147483647)]
    /*[IntentFilter(new[] { "android.intent.action.DATA_SMS_RECEIVED" }, DataPort = "5605", DataScheme = "sms", DataHost = "localhost", Priority = 2147483647)]*/
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = 2147483647)]
    public class SmsReceiver : BroadcastReceiver
    {
        public const string RINGTONE_PORT = "5505";
        public const string OPERATOR_LOGO_PORT = "5506";
        public const string CALLER_GROUP_GRAPHIC_PORT = "5507";

        public static readonly string INTENT_DATA = "android.intent.action.DATA_SMS_RECEIVED";

        public static readonly string INTENT_ACTION = "android.provider.Telephony.SMS_RECEIVED";

        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
            if (intent.Action == INTENT_DATA)
            {
                Log.Info("SmsReceiver", "Received data message");
            }

            if (intent.Action == INTENT_ACTION)
            {
                StringBuilder buffer = new StringBuilder();
                Bundle bundle = intent.Extras;

                if (bundle != null)
                {
                    Java.Lang.Object[] pdus = (Java.Lang.Object[])bundle.Get("pdus");

                    SmsMessage[] msgs;
                    msgs = new SmsMessage[pdus.Length];

                    for (int i = 0; i < msgs.Length; i++)
                    {
                        Log.Debug("SmsReceiver", ((byte[])pdus[i]).ToHexString());

                        /*msgs[i] = SmsMessage.CreateFromPdu((byte[])pdus[i]);

                        Log.Info("SmsReceiver", "SMS Received from: " + msgs[i].OriginatingAddress);
                        Log.Info("SmsReceiver", "SMS Data: " + msgs[i].MessageBody.ToString());*/
                    }

                    Log.Info("SmsReceiver", "SMS Received");
                }
            }
        }
    }
}
