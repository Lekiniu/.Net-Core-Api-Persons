using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using Persons.Api.MiddlewareServices;
using Persons.Data.Entities;
using Persons.Data.Interfaces;
using Persons.Core.Services;
using Newtonsoft.Json;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Persons.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddDbContext<PersonsDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:PersonsDb"]));
            services.AddHttpContextAccessor();
            services.AddSingleton<ILoggerService, LoggerMiddleware>();
            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<IFileService, FileServices>();
            services.AddScoped<IRelatedPersonServices, RelatedPersonsServices>();

            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]
            //          {
            //                new CultureInfo("en-US"),
            //                new CultureInfo("ka-Ge"),

            //            };

            //    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
            //    {
            //        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
            //        options.SupportedCultures = supportedCultures;
            //        options.SupportedUICultures = supportedCultures;

            //        var userLangs = context.Request.Headers["Accept-Language"].ToString();
            //        var firstLang = userLangs.Split(',').FirstOrDefault();
            //        var defaultLang = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
            //        return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
            //    }));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new[] { "en-US", "ka-Ge" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
