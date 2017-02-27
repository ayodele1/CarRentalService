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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;

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

            services.Configure<IdentityOptions>(config =>
            {
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (cae) =>
                    {
                        if (cae.Request.Path.StartsWithSegments("/api") && cae.Response.StatusCode == 200)
                        {
                            cae.Response.StatusCode = 401;
                        }
                        return Task.CompletedTask;
                    },

                    OnRedirectToAccessDenied = (cae) =>
                    {
                        if (cae.Request.Path.StartsWithSegments("/api") && cae.Response.StatusCode == 200)
                        {
                            cae.Response.StatusCode = 403;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

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
                config.CreateMap<ReservationContact, ReservationContactViewModel>();
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
                config.CreateMap<Reservation, ReservationViewModel>()
                .ForMember(dest => dest.LogisticsSetup, opt => opt.MapFrom(src => new ReservationLogisticsViewModel
                    {
                        PickupLocation = src.PickupLocation,
                        ReturnLocation = src.ReturnLocation,
                        PickupDate = src.PickupDate,
                        ReturnDate = src.ReturnDate,
                        UserLocation = src.UserLocation
                    })
                )
                .ForMember(dest => dest.VehicleSetup, opt => opt.MapFrom(src => new ReservationVehicleViewModel { VehicleId = src.VehicleId }));
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
            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404
            //    && !Path.HasExtension(context.Request.Path.Value))
            //    {
            //        context.Request.Path = "/index.html";
            //        await next();
            //    }
            //});
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateLifetime = true
               }
            });

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
