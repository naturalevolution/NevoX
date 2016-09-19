using System.Globalization;
using NevoX.Droid.Localization;
using NevoX.Localization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace NevoX.Droid.Localization
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-"); // turns pt_BR into pt-BR
            return new CultureInfo(netLanguage);
        }
    }
}