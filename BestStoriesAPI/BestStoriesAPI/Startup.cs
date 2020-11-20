using System;
using Domain.Acl;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BestStoriesAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(SetSwaggerOptions())
                    .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IBestStoriesService, BestStoriesService>();
            services.AddScoped<IStoriesAcl, StoriesAcl>();

        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerBasePath = Configuration.GetValue("SwaggerBasePath", "/BestStories");
            app.UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint($"{swaggerBasePath}/swagger/v1/swagger.json", "BestStories V1"))
               .UseHttpsRedirection()
               .UseMvc();
        }

        private static Action<SwaggerGenOptions> SetSwaggerOptions() =>
        options =>
        {
            options.SwaggerDoc("v1",
            new Info
            {
                Title = "Test Best Stories - Best Stories Api",
                Version = "v1",
                Description = "API to show first 20 Best Stories",
            });
        };
    }
}
