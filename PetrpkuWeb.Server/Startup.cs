using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PetrpkuWeb.NovellDirectoryLdap;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Server.Services;

namespace PetrpkuWeb.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRssService, RssService>();
            services.AddScoped<IAppUsersService, AppUsersService>();
            services.AddScoped<ISectionsService, SectionsServices>();
            services.AddScoped<IDutyService, DutyService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITypeService<Note>, NoteService>();
            services.AddScoped<ITypeService<Building>, BuildingsService>();
            services.AddScoped<ITypeService<Department>, DepartmentsService>();
            services.AddScoped<ITypeService<CssType>, CssTypeService>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

#if DEBUG
            services.AddScoped<ILdapAuthenticationService, FakeAuthenticationService>();

            services.AddDbContext<AppDbContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("MsSql_debug")));
#else
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MsSql_release")));

            services.Configure<LdapConfig>(Configuration.GetSection("ldap"));
            services.AddScoped<ILdapAuthenticationService, LdapAuthenticationService>();
#endif
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                    };
                });

            services.AddAuthorization();

            services.AddResponseCompression(options =>
             {
                 options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                     new[] { MediaTypeNames.Application.Octet });
             });

            //services.AddCors();
            services.AddHttpClient();
            services.AddMemoryCache();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseBlazorDebugging();
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //app.UseStaticFiles();
            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".m3u8"] = "application/x-mpegURL";

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "uploadfolder")),
                RequestPath = "/uploadfolder",
                ContentTypeProvider = provider
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
