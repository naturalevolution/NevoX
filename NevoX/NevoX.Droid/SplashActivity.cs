using Android.App;
using Android.OS;

namespace NevoX.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
         MainLauncher = true,
         NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}