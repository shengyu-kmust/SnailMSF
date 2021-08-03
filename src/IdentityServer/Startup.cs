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

            // 对于client和resource不经常变动的，推荐用配置文件存储它们，并在系统启动时加载到InMemoryXXX。而如果要建SAAS体系，则可存储在db里
            // 
            services.AddIdentityServer(opt =>
            {
                //opt.UserInteraction.LoginUrl = "";//default is /account/login，loginUrl的query里会根据returnUrl去跳转到登录成功后的返回界面，如配置returnUrl=authorization endpoint
            })
                .AddInMemoryIdentityResources(IdentityServer4TestConfig.IdentityResources)
                .AddInMemoryApiScopes(IdentityServer4TestConfig.ApiScopes)
                .AddInMemoryClients(IdentityServer4TestConfig.Clients)
                .AddTestUsers(IdentityServer4TestConfig.Users)
                .AddDeveloperSigningCredential();
            services.AddLocalApiAuthentication(); // 保护identityServer本身的某些api，详细用法参考官网

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
