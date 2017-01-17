using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CarRentalApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using CarRentalApplication.Models.ViewModels.Auth;
using CarRentalApplication.Repositories;
using CarRentalApplication.Services;
using CarRentalApplication.Models.ViewModels.Reservation;

namespace CarRentalApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();



            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            })
            .AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>();
            services.AddSingleton(Configuration);
            services.AddTransient<AppDbContextSeedData>();
            services.AddScoped<VehicleRepository>();
            services.AddScoped<ReservationRepository>();
            services.AddScoped<ReservationContactRepository>();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<ViewModelSesssionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContextSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            Mapper.Initialize(config =>
            {
                config.CreateMap<RegisterViewModel, AppUser>();
                config.CreateMap<ReservationContactViewModel, ReservationContact>();
                config.CreateMap<ReservationContactViewModel, Reservation>();
                config.CreateMap<ReservationLogisticsViewModel, Reservation>();
                config.CreateMap<ReservationVehicleViewModel, Reservation>();
                config.CreateMap<ReservationViewModel, Reservation>()
                .ForMember(dest => dest.PickupLocation, opt => opt.MapFrom(src => src.LogisticsSetup.PickupLocation))
                .ForMember(dest => dest.ReturnLocation, opt => opt.MapFrom(src => src.LogisticsSetup.ReturnLocation))
                .ForMember(dest => dest.PickupDate, opt => opt.MapFrom(src => src.LogisticsSetup.PickupDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.LogisticsSetup.ReturnDate))
                .ForMember(dest => dest.UserLocation, opt => opt.MapFrom(src => src.LogisticsSetup.UserLocation))
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleSetup.VehicleId));
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            seeder.EnsureSeedData().Wait();
        }
    }
}
