using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli_Assignment.BusinessLogic.Interfaces;
using Albelli_Assignment.DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Albelli_Assignment
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
            services.AddControllers();

            //     Business Logic Layer
            services.AddTransient<IBusinessLogic, BusinessLogic.BusinessLogic>();
			services.AddSingleton<BusinessLogic.IBinMinWidthCalculator, BusinessLogic.BinMinWidthCalculator>();

            //     Data Access Layer
            //     To use the physical database, comment out the following line and uncomment the other one
            services.AddTransient<IDataAccess, DataAccess.DataAccessMocked>();
            //services.AddTransient<IDataAccess, DataAccess.DataAccess>();

            services.AddTransient<Entities.DatabaseContext, Entities.DatabaseContext>(s =>
            {
                return new Entities.DatabaseContext(Configuration.GetConnectionString("DatabaseConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
