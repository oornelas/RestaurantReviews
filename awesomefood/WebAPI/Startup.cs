using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.DataAccess;
using AwesomeFood.Entities;
using AwesomeFood.Interactors;
using AwesomeFood.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AwesomeFood.WebAPI
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
            services.AddMvc();

            //Repositories
            var endpoint = Configuration.GetSection("Repositories:DocumentDBRepositoryConfig:RepositoryAddress").Value;
            var key = Configuration.GetSection("Repositories:DocumentDBRepositoryConfig:RepositoryPassword").Value;
            var databaseId = Configuration.GetSection("Repositories:DocumentDBRepositoryConfig:RepositoryName").Value;
            
            services.AddSingleton<IRepository<IUser>,DocumentDBRepository<IUser, User>>
                        (provider => new DocumentDBRepository<IUser,User>(endpoint, key, databaseId));
            services.AddSingleton<IRepository<IRestaurant>,DocumentDBRepository<IRestaurant, Restaurant>>
                        (provider => new DocumentDBRepository<IRestaurant,Restaurant>(endpoint, key, databaseId));
            services.AddSingleton<IRepository<IDish>,DocumentDBRepository<IDish, Dish>>
                        (provider => new DocumentDBRepository<IDish,Dish>(endpoint, key, databaseId));
            services.AddSingleton<IRepository<IDishReview>,DocumentDBRepository<IDishReview, DishReview>>
                        (provider => new DocumentDBRepository<IDishReview,DishReview>(endpoint, key, databaseId));

            //DataAccess
            services.AddScoped<IUserDataAccess,UserDataAccess>();
            services.AddScoped<IRestaurantDataAccess,RestaurantDataAccess>();
            services.AddScoped<IDishDataAccess,DishDataAccess>();
            services.AddScoped<IDishReviewDataAccess,DishReviewDataAccess>();

            //Interactors
            services.AddScoped<IUserInteractor,UserInteractor>();
            services.AddScoped<IRestaurantInteractor,RestaurantInteractor>();
            services.AddScoped<IDishInteractor,DishInteractor>();
            services.AddScoped<IDishReviewInteractor,DishReviewInteractor>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AwesomeFood API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeFood API V1");
            });

            app.UseMvc();
        }
    }
}
