using System;
using Domain.Data.DataModels;
using Domain.Data.ViewModels;
using Domain.Ef;
using Domain.Ef.Repository;
using Domain.Ef.SqLite.Repository;
using LynxMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Services;
using Services.Abstractions.Transformators;
using Services.Implementations.Services;
using Services.Implementations.Transformators;

namespace SimpleWebApi
{
    public class Startup
    {
        /// <summary>
        /// Храним конфигурацию приложения
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ITripTransformator, TripTransformator>();
            services.AddTransient<IUserTransformator, UserTransformator>();

            services.AddTransient<ITripService, TripService>();
            services.AddTransient<ILynxServiceProvider, LynxServiceProvider>();

            services.AddMvc()
                    .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            try
            {
                var connString = Configuration["ConnectionString"];

                services.AddDbContext<MyAppContext>(options =>
                    options.UseSqlite(connString));

                Console.WriteLine("DbContext good!");
            }
            catch (Exception)
            {
                Console.WriteLine("The application could not get the connection string configuration.");
            }

            services.DbInitialize();

            var build = services.BuildServiceProvider();

            services.AddLynxMapper(options =>
            {
                options.RegisterTransformator<TripViewModel, Trips>(build.GetService<ITripTransformator>().ToTripViewModel);
                options.RegisterTransformator<UserViewModel, Users>(build.GetService<IUserTransformator>().ToUserViewModel);
                options.RegisterTransformator<UserFullViewModel, Users>(build.GetService<IUserTransformator>().ToUserFullViewModel);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseLynxMapper();

            app.UseMvc();
        }
    }
}
