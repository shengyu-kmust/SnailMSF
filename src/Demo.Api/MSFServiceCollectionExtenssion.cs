using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Demo.Api
{
    public static class MSFServiceCollectionExtenssion
    {
        public static IServiceCollection AddSMFAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl"); // 这个为IdentityServer部署的地址
            //var audience = configuration.GetValue<string>("Audience");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";// 推荐，方式一：从oidc站点进行验证 
                //options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters { } // 方式二：如没有oidc站点，从本地的certificate进行验证

                //options.RequireHttpsMetadata = false;

                //options.Audience = audience; // todo 暂时注释
                options.TokenValidationParameters.ValidateAudience = false;
            });
            // todo，增加token introspection的支持
            return services;
        }

    }
}
