
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
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });
                c.OrderActionsBy(o => o.RelativePath);
                //ϵͳ������ʱ��ȥXML��ȡע��
                var xmlPath = Path.Combine(basePath, "CoreProject.API.xml");//��Ŀ�������õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�
                var xmlModelPath = Path.Combine(basePath, "CoreProject.Model.xml");
                c.IncludeXmlComments(xmlModelPath);
            });

            #endregion
  
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region ע��CoreProject.Services����IScanInfoServices ʵ��������ע�뵽��Autofac������
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //ֱ��ע��ĳһ����ͽӿ�
            //��ߵ���ʵ���࣬�ұߵ�As�ǽӿ�
            builder.RegisterType<ScanInfoServices>().As<IScanInfoServices>();
            //ע��������
            builder.RegisterType<LogAOP>();//�����������ӵ�Ҫע�������Ľӿڻ�����֮��
            //ע��Ҫͨ�����䴴�������
            var servicesDllFile = Path.Combine(basePath, "CoreProject.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);

            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(LogAOP));//
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

                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
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