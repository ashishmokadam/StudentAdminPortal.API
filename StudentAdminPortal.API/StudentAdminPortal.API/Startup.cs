using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdminPortal.API
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
            services.AddCors((options) =>
            {
                // Create a policy for angular application and it to services. 
                options.AddPolicy("angularApplication", (builder) =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .WithMethods("GET", "PUT", "POST", "DELETE")
                    .WithExposedHeaders("*");
                });
            });

            services.AddControllers();

            // Configured DbContext using DI from Startup
            services.AddDbContext<StudentAdminContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("StudentAdminPortalDb")));

            // Whenever I call this IStudentRepository plese give me an implementation of SqlStudentRepository. 
            // This will basically inject our dependecies inside the services which we can use back in our controller.
            services.AddScoped<IStudentRepository, SqlStudentRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentAdminPortal.API", Version = "v1" });
            });

            // Inject automapper so that it can read the maps created in profile folder.
            // When application runs it will go to Startup class and get its assembly name (which is this current application).
            // It will search for all the profiles (autmapper profiles) which have been inherited from profile class and it will create the maps for us. 
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentAdminPortal.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("angularApplication");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
