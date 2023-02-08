using System;
using System.Timers;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SimpleChrono
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Timer t = new Timer(1000);
        int time = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TableLayout tl = new TableLayout(this);

            /*CustomTextView hrs = new CustomTextView(this, "h"),
                           mins = new CustomTextView(this, "m"),
                           secs = new CustomTextView(this, "s");*/

            View[][] views = new View[][] {
                new View[] {
                    /*new TextView(this) { Text = "Horas:" },*/
                    new CustomTextView(this, "h", 1)
                }, new View[] {
                    /*new TextView(this) { Text = "Minutos:" },*/
                    new CustomTextView(this, "m", 2)
                }, new View[] {
                    /*new TextView(this) { Text = "Segundos:" },*/
                    new CustomTextView(this, "s", 3)
                }, new View[]
                {
                    new Button(this) { Text = "Start" },
                    new Button(this) { Text = "Clear" }
                }
            };

            views[3][1].Click += (sender, args) =>
            {
                for (int i = 0; i < 3; i++)
                    ((CustomTextView)views[i][0]).Time = 0;
            };
            views[3][0].Click += (sender, args) =>
            {
                if (((Button)sender).Text == "Start")
                {
                    views[3][1].PerformClick();
                    ((Button)sender).Text = "Stop";
                    t.Start();
                }
                else
                {
                    t.Stop();
                    ((Button)sender).Text = "Start";
                }
            };
            

            foreach (View[] viewArr in views)
            {
                TableRow tr = new TableRow(this)
                {
                    Orientation = Orientation.Horizontal,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
                };
                
                foreach (View v in viewArr)
                {
                    tr.AddView(v);
                }

                tl.AddView(tr);
            }

            t.Elapsed += (sender, args) =>
            {
                ((CustomTextView)views[0][0]).Time = time / 3600;
                ((CustomTextView)views[1][0]).Time = time / 60;
                ((CustomTextView)views[2][0]).Time = time % 60;
            };

            SetContentView(tl);
        }
    }
}

