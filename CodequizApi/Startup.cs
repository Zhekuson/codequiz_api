using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services;
using Services.Services.Classes;
using Repository.Repository.Interfaces;
using Repository.Repository.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CodequizApi.Features.Auth;
using Services.Services.Interfaces;
using Repository.Repository.Interfaces.Quizes;
using Repository.Repository.Classes.Quizes;
using Services.Services.Interfaces.Stats;
using Services.Services.Classes.Stats;
using System.Text;
using Repository.Repository.Interfaces.QuizAttempts;
using Repository.Repository.Classes.QuizAttempts;

namespace CodequizApi
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
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IStatsService, StatsService>();
            services.AddSingleton<IQuizRepository, QuizRepository>();
            services.AddSingleton<IQuestionsRepository, QuestionsRepository>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IQuizAttemptRepository, QuizAttemptRepository>();
           
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true, //издатель
                            ValidIssuer = AuthOptions.ISSUER, // валидация потребителя токена
                            ValidateAudience = true, // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true, // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), // валидация ключа 
                            ValidateIssuerSigningKey = true,
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
