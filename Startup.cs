using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesStoreApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShoesStoreApi.Data;

namespace ShoesStoreApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) {
            services.Configure<ApplicationSettings> (Configuration.GetSection ("ApplicationSettings"));
            services.AddDbContext<AuthenticationContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("ShoesStoreApiConnection")));
            services.AddDbContext<ShoesStoreApiContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("ShoesStoreApiConnection")));
            services.AddDefaultIdentity<ApplicationUser> ()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext> ();
            services.Configure<IdentityOptions> (options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
            // services.AddAuthorization (options => {
            //     options.DefaultPolicy = new AuthorizationPolicyBuilder ()
            //         .RequireAuthenticatedUser ()
            //         .Build ();
            // });
            services.AddCors ();
            //Jwt Authentication
            var key = Encoding.UTF8.GetBytes (Configuration["ApplicationSettings:JWT_Secret"].ToString ());
            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            app.Use (async (ctx, next) => {
                await next ();
                if (ctx.Response.StatusCode == 204) {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCors (builder =>
                builder.AllowAnyHeader ()
                .AllowAnyMethod ()
                .WithOrigins (Configuration["ApplicationSettings:Client_URL"].ToString ())
            );
            //app.UseCors(builder =>
            //   builder.AllowAnyHeader()
            //   .AllowAnyMethod()
            //   .WithOrigins(Configuration["ApplicationSettings:Client_URL2"].ToString())
            //);
            app.UseRouting ();

            app.UseAuthentication ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}