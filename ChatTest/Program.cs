using ChatTest.Configuration;
using ChatTest.Database;
using ChatTest.Services.EventBus;
using ChatTest.Services.TokenHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ChatTest
{

    /*
        eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZXhwIjoxNzk5NDkzNzU0fQ.AGcGH3w3QRywu_fx6j8TfQ8v2z19dflCNfUyT4cNsgQ
        eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyIiwiZXhwIjoxNzk5NDkzNzU0fQ.Fv5Ic_RQqiRVjAUhUx-F6mT_aeHantWVN4AbgqEiccQ
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddCors();
            builder.Services.AddDbContext<DefaultContext>(x => x.UseSqlite("Data Source=chat.db;"));
            builder.Services.AddSingleton(typeof(IEventBus<>), typeof(EventBus<>));

            builder.Services.AddSwaggerGen(c =>
            {
                // Bearer token authentication
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
                c.AddSecurityRequirement(securityRequirements);
            });

            builder.Services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            builder.Services.AddSingleton<ITokenHelper, TokenHelper>();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    JwtAuthOptions jwtAuthOptions = new JwtAuthOptions();
                    builder.Configuration.GetSection("JwtAuth").Bind(jwtAuthOptions);
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAuthOptions.AccessSigningKey));
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        IssuerSigningKey = key,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateActor = false,
                    };
                });

            var app = builder.Build();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                //x.WithOrigins(new[] { "http://localhost:5173" });
                //x.AllowCredentials();
            });

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            });

            app.UseFileServer();
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies[".AspNetCore.Application.Id2"];
                if (!string.IsNullOrEmpty(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                await next();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();

            app.UseEndpoints(e => { });

            app.MapControllers();

            string index = File.ReadAllText("wwwroot/index.html", Encoding.UTF8);

            app.Run(async x =>
            {
                x.Response.ContentType = "text/html; charset=utf-8";
                await x.Response.WriteAsync(index);
            });

            app.Run();
        }
    }
}