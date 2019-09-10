using Blazor.FileReader;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using PetrpkuWeb.Client.Extensions;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using AutoMapper;

namespace PetrpkuWeb.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddTransient<IAppVersionService, AppVersionService>();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            //app.SetCultureInfo("ru-RU");

            app.UseLocalTimeZone();

            app.AddComponent<App>("app");
        }
    }
}
