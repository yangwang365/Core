
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Services;
using IServices;
using Repository;
using Mes.Core.Extensions;
using Autofac;
using System.Reflection;
using Autofac.Extras.DynamicProxy;

namespace Mes.Core
{
    public class Startup
    {
        public string ApiName { get; set; } = "Mes.Core";
        


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.Data Source=server;Initial Catalog=db;User ID=test;Password=test
        public void ConfigureServices(IServiceCollection services)
        {
            //BaseDBConfig.ConnectionString = Configuration.GetSection("AppSettings:SqlServerConnection").Value;
            //services.AddDbContext<MyContext>(opt => opt.UseSqlServer("Data Source=106.13.125.162;Initial Catalog=MyTestDb;User ID=yuhong;Password=123456"));
            services.AddControllers();
            //services.AddSqlsugarSetup();
            #region Swagger UI Service

            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);
                //系统启动的时候去XML读取注释
                var xmlPath = Path.Combine(basePath, "Mes.Core.xml");//项目属性配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                var xmlModelPath = Path.Combine(basePath, "Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });

            #endregion
  
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region 注入Services，把IScanInfoServices 实例化过程注入到了Autofac容器中
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //直接注册某一个类和接口
            //左边的是实现类，右边的As是接口
            builder.RegisterType<ScanInfoServices>().As<IScanInfoServices>();
            //注册要通过反射创建的组件
            var servicesDllFile = Path.Combine(basePath, "Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors();
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
