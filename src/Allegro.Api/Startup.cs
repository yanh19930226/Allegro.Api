using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Allegro.Api.Application.Services;
using Allegro.Api.Application.Services.Impl;
using Allegro.SDK;
using Core;
using Core.Logger;
using Core.Redis;
using Core.Swagger;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetSection("Zeus:Connection").Value, sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });

            services.Configure<Appsettings>(Configuration.GetSection("Appsettings"));
            var settings = services.BuildServiceProvider().GetService<IOptions<Appsettings>>().Value;
            var client = new HttpClient();
            services.AddSingleton(new AllegroClient(client, settings.Allegro.ClientId, settings.Allegro.ClientSecret, settings.Allegro.IsDev ? EnvEnum.Dev : EnvEnum.Prod));

            #region  ToDo 批量注入
            services.AddScoped<IAllegroAuthService, AllegroAuthService>();
            services.AddScoped<IAllegroCategoryService, AllegroCategoryService>();
            services.AddScoped<IAllegroProductService, AllegroProductService>();

            #endregion

            services.AddCoreSwagger()
                        .AddRedis()
                        .AddCoreSeriLog();

            #region Polly
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
            #endregion

            services.AddHangfire(config =>
            {
                var str = Configuration.GetSection("Hangfire:Connection").Value;
                config.UseStorage(
                    new MySqlStorage(Configuration.GetSection("Hangfire:Connection").Value,
                    new MySqlStorageOptions
                    {

                        #region MyRegion
                        //TransactionIsolationLevel = IsolationLevel.ReadCommitted, // 事务隔离级别。默认是读取已提交。
                        //QueuePollInterval = TimeSpan.FromSeconds(15),             //- 作业队列轮询间隔。默认值为15秒。
                        //JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                        //CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- 聚合计数器的间隔。默认为5分钟。
                        //PrepareSchemaIfNecessary = true,                          //- 如果设置为true，则创建数据库表。默认是true。
                        //DashboardJobListLimit = 50000,                            //- 仪表板作业列表限制。默认值为50000。
                        //TransactionTimeout = TimeSpan.FromMinutes(1),             //- 交易超时。默认为1分钟。 
                        #endregion

                    }));
            });

        }

        public override void CommonConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCoreSwagger();
            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                IgnoreAntiforgeryToken = true,
                AppPath = "#",//返回时跳转的地址
                DisplayStorageConnectionString = false,//是否显示数据库连接信息
                IsReadOnlyFunc = Context =>
                {
                    return true;
                },
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,//是否启用ssl验证，即https
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users = new []
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = "read",
                            PasswordClear = "only"
                        },
                        new BasicAuthAuthorizationUser
                        {
                            Login = "yanh",
                            PasswordClear = "123"
                        },
                        new BasicAuthAuthorizationUser
                        {
                            Login = "guest",
                            PasswordClear = "123@123"
                        }
                    }
                })
                },
                DashboardTitle = "任务调度中心"
            });

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Minutely());

        }
    }
}
