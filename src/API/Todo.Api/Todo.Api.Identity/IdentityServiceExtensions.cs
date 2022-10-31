using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Todo.Api.Identity.Models;
using Todo.Api.Identity.Services;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Application.Models.Authentication;
using Todo.Api.Application.Contracts.Identity;
using Microsoft.AspNetCore.Http;

namespace Todo.Api.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<TodoApiIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TodoApiIdentityConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = false
                }
                )
                .AddEntityFrameworkStores<TodoApiIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["JwtSettings:AuthorityTodo"];
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(o =>
            //    {
            //        o.RequireHttpsMetadata = false;
            //        o.SaveToken = false;
            //        o.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.Zero,
            //            ValidIssuer = configuration["Jwt:JwtSettings"],
            //            ValidAudience = configuration["Jwt:ValidAudience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            //        };

            //        o.Events = new JwtBearerEvents()
            //        {
            //            OnAuthenticationFailed = c =>
            //            {
            //                c.NoResult();
            //                c.Response.StatusCode = 500;
            //                c.Response.ContentType = "text/plain";
            //                return c.Response.WriteAsync(c.Exception.ToString());
            //            },
            //            OnChallenge = context =>
            //            {
            //                context.HandleResponse();
            //                context.Response.StatusCode = 401;
            //                context.Response.ContentType = "application/json";
            //                var result = JsonConvert.SerializeObject("401 Not authorized");
            //                return context.Response.WriteAsync(result);
            //            },
            //            OnForbidden = context =>
            //            {
            //                context.Response.StatusCode = 403;
            //                context.Response.ContentType = "application/json";
            //                var result = JsonConvert.SerializeObject("403 Not authorized");
            //                return context.Response.WriteAsync(result);
            //            },
            //        };
            //    });

            return services;
        }
    }
}