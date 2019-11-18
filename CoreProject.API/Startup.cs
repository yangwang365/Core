
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreProject.Model;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using CoreProject.Services;
using CoreProject.IServices;
using CoreProject.Repository;
using CoreProject.API.Extensions;
using Autofac;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using CoreProject.API.AOP;
using CoreProject.Common.CacheHelper;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using CoreProject.Common.Redis;
using CoreProject.Common;

namespace CoreProject.API
{
    public class Startup
    {
        public string ApiName { get; set; } = "CoreProject.API";



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.Data Source=server;Initial Catalog=db;User ID=test;Password=test
        public void ConfigureServices(IServiceCollection services)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
            //services.AddDbContext<MyContext>(opt => opt.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=MyTestDb;User ID=yuhong;Password=123456"));
            services.AddControllers();
            //services.AddSqlsugarSetup();
            //缓存注入
            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            services.AddSingleton(new Appsettings(basePath));
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——基于Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "hong.yu@finern.com", Url = new Uri("https://www.baidu.com") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.baidu.com") }
                });
                c.OrderActionsBy(o => o.RelativePath);
                //系统启动的时候去XML读取注释
                var xmlPath = Path.Combine(basePath, "CoreProject.API.xml");//项目属性配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(basePath, "CoreProject.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });
  
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region 注入CoreProject.Services，把IScanInfoServices 实例化过程注入到了Autofac容器中
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //直接注册某一个类和接口
            //左边的是实现类，右边的As是接口
            builder.RegisterType<ScanInfoServices>().As<IScanInfoServices>();
            //注册Log拦截器
            builder.RegisterType<LogAOP>();//将拦截器添加到要注入容器的接口或者类之上
            builder.RegisterType<MemoryCacheAOP>();
            builder.RegisterType<RedisCacheAOP>();
            //注册要通过反射创建的组件
            var servicesDllFile = Path.Combine(basePath, "CoreProject.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            List<Type> AOPList =new List<Type> { typeof(LogAOP), typeof(MemoryCacheAOP), typeof(RedisCacheAOP) };
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(AOPList.ToArray());//放入拦截器集合
            #endregion
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
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");

                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
