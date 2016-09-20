﻿
using Xamarin.Forms;

namespace NevoX.Statics
{
    public static class Thicknesses
    {
        public static Thickness Empty
        {
            get{ return new Thickness(0, 0, 0, 0); }
        }

        public static Thickness IosStatusBar
        {
            get{ return new Thickness(0, 20, 0, 0); }
        }

        public static Thickness StackLayout2Buttons
        {
            get { return new Thickness(60, 20, 60, 0); }
        }
    }
}

