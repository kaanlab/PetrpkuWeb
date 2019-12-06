using Blazor.FileReader;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using PetrpkuWeb.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using MatBlazor;
using Ganss.XSS;
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
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 93;
                //config.VisibleStateDuration = 7000;
            });
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
            {
                // Configure sanitizer rules as needed here.
                // For now, just use default rules + allow class attributes
                var sanitizer = new Ganss.XSS.HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });
            services.AddAutoMapper(typeof(WebClientMappingProfile));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.SetCultureInfo("ru-RU");

            //app.UseLocalTimeZone();

            app.AddComponent<App>("app");
        }
    }
}
