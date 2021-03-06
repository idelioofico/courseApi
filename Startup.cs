using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using courseApi.Business.Repositories;
using courseApi.Configurations;
using courseApi.Infraestructure.Data;
using courseApi.Infraestructure.Repositories;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace courseApi
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

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //Disable model state validation
                    options.SuppressModelStateInvalidFilter = true;
                });
            //Gets the secret under the appsettings confi file
            var chave = Configuration.GetSection("JwtConfigurations:Secret").Value;
            var secret = Encoding.ASCII.GetBytes(chave);

            //Register the authentication midleware
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    //Disable https
                    ValidateIssuerSigningKey = true,

                    //Use symetrickey
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    //Use local jwt
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer schema (Example: 12345abcdf)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{

                            Reference=new OpenApiReference{
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course API",Description="Api for register user courses", Version = "v1" });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);

            });

            services.AddDbContext<ApplicationDbContext>(options => {

                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, JwtService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course Manager API - v1");
                c.RoutePrefix = String.Empty;//Swagger
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
