using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quorum.OnDemand.Importer.Core.Repository;
using System.Text.Json.Serialization;
using TravelAgency.Core.Repository;
using TravelAgency.Helpers;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.Middlewares;
using TravelAgency.Services;

namespace TravelAgency
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

            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //AddJsonOptions(x =>
            //{
            //    //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //    // serialize enums as strings in api responses (e.g. Role)
            //    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelAgency", Version = "v1" });
            });

            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITransportRepository, TransportRepository>();
            services.AddTransient<ITransportationRepository, TransportationRepository>();
            services.AddTransient<ITourTypeRepository, TourTypeRepository>();
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<ITourRepository, TourRepository>();
            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<ICheckRepository, CheckRepoitory>();
            services.AddTransient<IRoleRepository, RoleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelAgency v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
