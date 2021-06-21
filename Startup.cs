using ATS.API.Data.Base;
using ATS.API.Data.Repository;
using ATS.API.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ATS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            _ = services.AddControllers();
            _ = services.AddResponseCompression();
            _ = services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Swagger Demo",
                        Description = "Swagger Demo for ValuesController"
                    });
                    c.UseAllOfForInheritance();
                    c.UseOneOfForPolymorphism();
                });

            _ = services.AddScoped<BlobFileService>();
            _ = services.AddScoped<JobService>();
            _ = services.AddScoped<CandidateService>();            
            _ = services.AddScoped<CandidateJobService>();

            _ = services.AddScoped<JobRepository>();
            _ = services.AddScoped<CandidateRepository>();
            _ = services.AddScoped<CandidateJobRepository>();            

            _ = services.AddTransient(_ => new SqlHelper(Configuration));
            _ = services.AddTransient(_ => new BlobHelper(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _ = app.UseRouting();
            _ = app.UseCors("Policy1");
            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
        }
    }
}
