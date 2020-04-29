using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API {
    public class Startup {
        private readonly IConfiguration _config;
        public Startup (IConfiguration config) {
            _config = config;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            
            services.AddScoped<IProductRepository, ProductRepository> ();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers ();
            var connectionString = _config.GetConnectionString ("MSSQLConn");
            
            services.AddDbContext<AcessoIngressoContext> (o => o.UseSqlServer (connectionString, 
                                                x => x.MigrationsAssembly("Migrations")));
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionConext =>
                {
                    var errors = actionConext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApliValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}