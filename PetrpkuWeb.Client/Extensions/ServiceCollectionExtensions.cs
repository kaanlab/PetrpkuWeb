using System.Globalization;
using Microsoft.AspNetCore.Components.Builder;

namespace PetrpkuWeb.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetCultureInfo(this IComponentsApplicationBuilder app, string locale)
        {
            var cultureInfo = new CultureInfo(locale);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
