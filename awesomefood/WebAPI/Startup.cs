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

            //DataAccess
            services.AddScoped<IUserDataAccess,UserDataAccess>();

            //Interactors
            services.AddScoped<IUserInteractor,UserInteractor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
