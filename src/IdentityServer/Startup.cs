using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
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

            services.AddControllers();

            // asp.net core identity
            //services.AddIdentityCore<User>(opt =>
            //{

            //});

            // ����client��resource�������䶯�ģ��Ƽ��������ļ��洢���ǣ�����ϵͳ����ʱ���ص�InMemoryXXX�������Ҫ��SAAS��ϵ����ɴ洢��db��
            // 
            services.AddIdentityServer(opt =>
            {
                //opt.UserInteraction.LoginUrl = "";//default is /account/login��loginUrl��query������returnUrlȥ��ת����¼�ɹ���ķ��ؽ��棬������returnUrl=authorization endpoint
            })
                .AddInMemoryIdentityResources(IdentityServer4TestConfig.IdentityResources)
                .AddInMemoryApiScopes(IdentityServer4TestConfig.ApiScopes)
                .AddInMemoryClients(IdentityServer4TestConfig.Clients)
                .AddTestUsers(IdentityServer4TestConfig.Users)
                .AddDeveloperSigningCredential();
            services.AddLocalApiAuthentication(); // ����identityServer�����ĳЩapi����ϸ�÷��ο�����

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
