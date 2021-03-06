using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dream.Shop.DataEntity;
using Dream.Shop.IService;
using Dream.Shop.Service;
using Dream.Shop.WebApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;

namespace Dream.Shop.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<DbContext,Dream_ShopContext>();
            services.AddMemoryCache();//使用缓存服务
            services.Configure<EmailMod>(Configuration.GetSection("EmailMod"));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPersonalService, PersonalService>();
            services.AddScoped<IDetailsPageService, DetailsPageService>();
            services.AddScoped<IHomePageService, HomePageService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILogingService, LoginService>();


            services.AddDbContext<Dream_ShopContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnection"));
            });

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "DemoAPI",
                    Version = "v1"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "WebApi.xml");
                setup.IncludeXmlComments(xmlPath);
            });
            services.AddCors(setup =>
            {
                setup.AddPolicy("corecors", config =>
                {
                    //config.AllowAnyOrigin()//允许所有跨域

                    config.AllowAnyMethod()//所有请求头方法
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()//所有请求头
                    .AllowCredentials();//玄虚所欲cookie
                    
                    
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

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreAPI Docs");
            });
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //    options.AllowCredentials();
            //});
            app.UseRouting();
            app.UseCors("corecors");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
