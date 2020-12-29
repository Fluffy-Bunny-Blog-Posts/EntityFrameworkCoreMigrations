using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Options;
using HostApp.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SchoolEntityFrameworkCore;

namespace HostApp
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
            services.AddRazorPages();
            
            var entityFrameworkConnectionOptions = Configuration
                .GetSection("EntityFrameworkConnectionOptions")
                .Get<EntityFrameworkConnectionOptions>();
            services.Configure<EntityFrameworkConnectionOptions>(Configuration.GetSection("EntityFrameworkConnectionOptions"));
            services.AddScoped<TenantInfo>();
            services.AddSingleton<IMigrationsAssemblyProvider, SqlServerMigrationsAssemblyProvider>();
            services.AddTransient<ITenantAwareDbContextOptionsProvider, SqlServerDbContextOptionsProvider>();
            services.TryAddSingleton<ITenantAwareSchoolContextAccessor, TenantAwareSchoolContextAccessor>();
            services.AddDbContext<SchoolContext>((serviceProvider, optionsBuilder) => {
                // for NON-INMEMORY  - TenantAwareConfigurationDbContext
                // this is only here so that migration models can be created.
                // we then use it as a template to not only create the new database for the tenant, but
                // downstream using it as a normal connection.

                var dbContextOptionsProvider = serviceProvider.GetRequiredService<ITenantAwareDbContextOptionsProvider>();
                dbContextOptionsProvider.Configure(optionsBuilder);
            });

            services.AddSingleton(new ConfigurationStoreOptions());
            services.AddSingleton(new OperationalStoreOptions
            {
                EnableTokenCleanup = true
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
