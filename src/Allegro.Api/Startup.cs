using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Allegro.Api.Application.Services;
using Allegro.Api.Application.Services.Impl;
using Allegro.SDK;
using Core;
using Core.Logger;
using Core.Redis;
using Core.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace Allegro.Api
{
    public class Startup : CommonStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void CommonServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseMySql(Configuration.GetConnectionString("MysqlUser"), sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            //});


            services.Configure<Appsettings>(Configuration.GetSection("Appsettings"));
            var settings = services.BuildServiceProvider().GetService<IOptions<Appsettings>>().Value;
            var client = new HttpClient();
            services.AddSingleton(new AllegroClient(client, settings.Allegro.ClientId, settings.Allegro.ClientSecret, settings.Allegro.IsDev ? EnvEnum.Dev : EnvEnum.Prod));

            #region  ToDo 批量注入
            services.AddScoped<IAllegroAuthService, AllegroAuthService>();
            services.AddScoped<IAllegroCategoryService, AllegroCategoryService>(); 
            #endregion

            services.AddCoreSwagger()
                        .AddRedis()
                        .AddCoreSeriLog();

            //services.AddHttpClient<AllegroClient>().AddPolicyHandler((services, request) => HttpPolicyExtensions.HandleTransientHttpError()
            //                                                    .WaitAndRetryAsync(new[]
            //                                                    {
            //                                                         TimeSpan.FromSeconds(1),
            //                                                         TimeSpan.FromSeconds(5),
            //                                                         TimeSpan.FromSeconds(10)
            //                                                    },
            //                                                    onRetry: (outcome, timespan, retryAttempt, context) =>
            //                                                    {
            //                                                        services.GetService<ILogger<AllegroClient>>()?.LogWarning("延迟 {delay}ms, 重新重试 {retry}.", timespan.TotalMilliseconds, retryAttempt);
            //                                                    }));

        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCoreSwagger();
        }
    }
}
