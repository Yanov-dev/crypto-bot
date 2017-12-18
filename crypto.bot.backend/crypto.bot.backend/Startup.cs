using System;
using AutoMapper;
using crypto.bot.backend.Background;
using crypto.bot.backend.dto;
using crypto.bot.backend.Models;
using crypto.bot.backend.Options;
using crypto.bot.backend.Repositories.Currency;
using crypto.bot.backend.Repositories.Trigger;
using crypto.bot.backend.Services.Auth;
using crypto.bot.backend.Services.CurrencySource;
using crypto.bot.backend.Services.Telegram.TelegramApiService;
using crypto.bot.backend.Services.Telegram.TelegramCommandProcessor;
using crypto.bot.backend.Services.Token;
using crypto.bot.backend.Services.TriggerServices.TriggerChecker;
using crypto.bot.backend.Services.TriggerServices.TriggerConverterService;
using crypto.bot.backend.Services.TriggerServices.TriggerProccesor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace crypto.bot.backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Mapper.Initialize(e => { e.CreateMap<CurrencyDto, CurrencyInfo>(); });

            services.AddMvc();

            // background
            services.AddSingleton<IHostedService, CryptoCheckHostedService>();
            services.AddSingleton<IHostedService, TelegramBotHostedService>();


            services.AddSingleton<ITokenService, TokenService>();

            // db
            services.AddSingleton<ICurrencyRepository, CurrencyRepository>();
            services.AddSingleton<ITriggerRepository, TriggerRepository>();

            // options
            services.Configure<TelegramOptions>(Configuration.GetSection(nameof(TelegramOptions)));
            services.Configure<AuthOptions>(Configuration.GetSection(nameof(AuthOptions)));
            services.Configure<RepositoryOptions>(Configuration.GetSection(nameof(RepositoryOptions)));

            services.AddSingleton<ITriggerConverterService, TriggerConverterService>();
            services.AddSingleton<ITriggerProcessor, TriggerProcessor>();
            services.AddSingleton<ITriggerCheckerService, TriggerCheckerService>();

            services.AddSingleton<ITelegramApiService, TelegramApiService>();
            services.AddSingleton<ITelegramCommandProcessor, TelegramCommandProcessor>();

            services.AddSingleton<ICurrencySource, CurrencySource>();

            var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();

            if (!authOptions.IsValid)
                throw new Exception("auth options is invalid");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddSingleton<IAuthService, AuthService>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}