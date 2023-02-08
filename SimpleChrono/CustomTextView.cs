using Android.Content;
using Android.Widget;

namespace SimpleChrono
{
    class CustomTextView : TextView
    {
        private readonly string suffix;
        private int time;
        public int Time
        {
            get => time;
            set
            {
                time = value;
                Text = time + suffix;
            }
        }

        public CustomTextView(Context context, string suffix, int id) : base(context)
        {
            this.suffix = suffix;
            Time = 0;
            Id = id;
        }
    }
}