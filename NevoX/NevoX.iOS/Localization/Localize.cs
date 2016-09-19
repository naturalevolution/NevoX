using System.Globalization;
using Foundation;
using NevoX.iOS.Localization;
using NevoX.Localization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace NevoX.iOS.Localization
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = pref.Replace("_", "-"); // turns es_ES into es-ES

                if (pref == "pt")
                    pref = "pt-BR"; // get the correct Brazilian language strings from the PCL RESX
                //(note the local iOS folder is still "pt")
            }
            return new CultureInfo(netLanguage);
        }
    }
}