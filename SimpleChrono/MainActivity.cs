using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidColor = Android.Graphics.Color;

namespace SimpleChrono
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenLayout | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        readonly Timer t = new Timer(100);
        int time = 0, temp = 0;
        bool active = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            t.Elapsed += UpdateTimer;

            TextView[] text = new TextView[3]
            {
                FindViewById<TextView>(Resource.Id.Hrs),
                FindViewById<TextView>(Resource.Id.Mins),
                FindViewById<TextView>(Resource.Id.Secs)
            };
            Button reset = FindViewById<Button>(Resource.Id.Reset);
            reset.Click += delegate
            {
                time = -1;
                UpdateTimer(null, null);
            };

            LinearLayout back = FindViewById<LinearLayout>(Resource.Id.BackLayout);
            FindViewById<LinearLayout>(Resource.Id.BackLayout).Click += delegate
            {
                if (!active)
                {
                    reset.Enabled = false;
                    for (int i = 0; i < 3; i++) text[i].SetTextColor(AndroidColor.Argb(0xFF, 0xFF, 0x44, 0x4B));
                    active = true;
                    t.Start();
                }
                else
                {
                    t.Stop();
                    active = false;
                    for (int i = 0; i < 3; i++) text[i].SetTextColor(AndroidColor.Argb(0xFF, 0x44, 0x58, 0xFF));
                    reset.Enabled = true;
                }
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
        }

        protected override void OnPause()
        {
            base.OnPause();
            Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);

            UpdateTimer(null, null);
            if (!active) {
                active = true;
                t.Start();
            }
        }

        private void UpdateTimer(object sender, ElapsedEventArgs e)
        {
            time++;
            temp = time / 10;
            RunOnUiThread(() =>
            {
                FindViewById<TextView>(Resource.Id.Hrs).Text = string.Format("{0:00}h", temp / 3600);
                FindViewById<TextView>(Resource.Id.Mins).Text = string.Format("{0:00}m", (temp % 3600) / 60);
                FindViewById<TextView>(Resource.Id.Secs).Text = string.Format("{0:00}s", temp % 60);
            });
        }
    }
}