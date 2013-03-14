/*
 * Copyright (C) 2009 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Util;

namespace ClassicPhone
{
	[Service]
	public class UpdateService : Service
	{
		private Context context;

		public override void OnStart (Intent intent, int startId)
		{
			RemoteViews updateViews = buildUpdate (this);
            ComponentName thisWidget = new ComponentName(this, "classicphone.ClassicPhoneWidget");
			AppWidgetManager manager = AppWidgetManager.GetInstance (this);
			manager.UpdateAppWidget (thisWidget, updateViews);
		}

		public override IBinder OnBind (Intent intent)
		{
			// We don't need to bind to this service
			return null;
		}

		// Build a widget update to show the current Wiktionary
		// "Word of the day." Will block until the online API returns.
        public RemoteViews buildUpdate(Context context)
        {
            // Build an update that holds the updated widget contents
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_logo);

            Log.Debug("WIDGET", "Loading OTA");

            var ota = Arasoft.ClassicPhone.OTABitmap.FromUserData("00 48 1C 01 7F FF EF FF EF FF FB FF FE 40 3F E8 38 2F FF FB FF FE 48 3F A8 38 2F 9F FB FF FE 4C FF A9 FF 2F 8F FA DA DA 4E FF 29 01 2F 80 FA 52 52 5E 7F 69 31 2F BF 7B 07 06 4F FF 69 79 2F BE FB 77 76 47 FF 69 79 2F BE 7B 07 06 47 FE EF 7D EF BE 7B FF FE 47 FC EF 7D E7 BC F1 FF FC 40 F0 EF 7D E7 7C F1 ED BC 21 E7 C9 79 27 98 F1 E5 3C 21 E7 C9 39 27 C8 F1 F0 7C 16 6F 89 39 23 E6 E0 F7 78 15 2F 88 82 23 F3 E0 F0 78 08 3F 04 44 43 D7 E0 FF F8 04 3E 02 28 81 EF C0 7F F0 02 3C 01 39 00 FF 80 3F E0 01 38 00 BA 00 7F 00 1F C0 00 F0 00 7C 00 3E 00 0F 80 FF C0 00 38 00 1C 00 07 FF 55 FF FF FF FF FF FF FF AA 2A F3 87 87 3F 1E 67 0F 54 15 F3 93 9F 3E 4E 27 27 A8 2A F3 87 8F 3E 4E 07 27 54 55 F3 93 9F 3E 0E 47 27 AA FF F3 9B 87 0E 4E 67 0F FF 00 FF FF FF FF FF FF FF 00".Replace(" ", ""));

            var bmp = Bitmap.CreateBitmap(10 + ota.Width * 4, 10 + ota.Height * 4, Bitmap.Config.Argb8888);
            var cnv = new Canvas(bmp);
            var white = new Paint(PaintFlags.AntiAlias) { Color = Color.White };
            var dropshadow = new Paint(PaintFlags.AntiAlias) { Color = Color.Argb(127, 0, 0, 0) };

            Log.Debug("WIDGET", "Drawing bitmap");

            DrawOTA(ota, cnv, dropshadow, 6, 6);
            DrawOTA(ota, cnv, white, 5, 5);

            Log.Debug("WIDGET", "Bitmap generated");

            updateViews.SetImageViewBitmap(Resource.Id.logo, bmp);

            Log.Debug("WIDGET", "View bitmap set");

            return updateViews;
        }

        private static void DrawOTA(Arasoft.ClassicPhone.OTABitmap ota, Canvas cnv, Paint white, int off_x, int off_y)
        {
            for (var x = 0; x < ota.Width; x++)
            {
                for (var y = 0; y < ota.Height; y++)
                {
                    if (ota.Bits[x, y])
                        cnv.DrawRect(new Rect(
                            off_x + x * 4,
                            off_y + y * 4,
                            off_x + x * 4 + 3,
                            off_y + y * 4 + 3), white);
                }
            }
        }
    }
}
