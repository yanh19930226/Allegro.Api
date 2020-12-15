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

            #region  ToDo ����ע��
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
            //                                                        services.GetService<ILogger<AllegroClient>>()?.LogWarning("�ӳ� {delay}ms, �������� {retry}.", timespan.TotalMilliseconds, retryAttempt);
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
                        //TransactionIsolationLevel = IsolationLevel.ReadCommitted, // ������뼶��Ĭ���Ƕ�ȡ���ύ��
                        //QueuePollInterval = TimeSpan.FromSeconds(15),             //- ��ҵ������ѯ�����Ĭ��ֵΪ15�롣
                        //JobExpirationCheckInterval = TimeSpan.FromHours(1),       //- ��ҵ���ڼ������������ڼ�¼����Ĭ��ֵΪ1Сʱ��
                        //CountersAggregateInterval = TimeSpan.FromMinutes(5),      //- �ۺϼ������ļ����Ĭ��Ϊ5���ӡ�
                        //PrepareSchemaIfNecessary = true,                          //- �������Ϊtrue���򴴽����ݿ��Ĭ����true��
                        //DashboardJobListLimit = 50000,                            //- �Ǳ����ҵ�б����ơ�Ĭ��ֵΪ50000��
                        //TransactionTimeout = TimeSpan.FromMinutes(1),             //- ���׳�ʱ��Ĭ��Ϊ1���ӡ� 
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
                AppPath = "#",//����ʱ��ת�ĵ�ַ
                DisplayStorageConnectionString = false,//�Ƿ���ʾ���ݿ�������Ϣ
                IsReadOnlyFunc = Context =>
                {
                    return true;
                },
                Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,//�Ƿ�����ssl��֤����https
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
                DashboardTitle = "�����������"
            });

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Minutely());

        }
    }
}
