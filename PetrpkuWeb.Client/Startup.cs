using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using PetrpkuWeb.Client.Extensions;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace PetrpkuWeb.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddTransient<IAppVersionService, AppVersionService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            //app.SetCultureInfo("ru-RU");

            app.UseLocalTimeZone();

            app.AddComponent<App>("app");
        }
    }
}
