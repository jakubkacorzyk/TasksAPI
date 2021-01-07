using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
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
using TasksCORE.DTOs;
using TasksCORE.Helpers;
using TasksCORE.Services.TaskGroups;
using TasksCORE.Services.UserTasks;
using TasksCORE.Validators;
using TasksDAL;

namespace TasksAPI
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
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter())).AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddDbContext<TasksDbContext>(options =>
            options.UseSqlite("Data Source = tasks.db"));

            services.AddCors(o => o.AddPolicy("CORSPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState);

                    var result = new BadRequestObjectResult(problemDetails);

                    result.ContentTypes.Add("application/problem+json");
                    result.ContentTypes.Add("application/problem+xml");

                    return result;
                };
            });

            services.AddAutoMapper(typeof(AutoMapperProfiles).GetTypeInfo().Assembly);

            services.AddTransient<IValidator<UserTaskForCreationDto>, UserTaskValidator>();

            services.AddScoped<ITaskGroupsService, TaskGroupsService>();
            services.AddScoped<IUserTasksService, UserTasksService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tasks API",
                    Version = "v1"
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORSPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
