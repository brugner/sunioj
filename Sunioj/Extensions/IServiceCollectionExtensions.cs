using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Services;
using Sunioj.Data.Repositories;
using Sunioj.Data.Services;
using Sunioj.Data.UnitsOfWork;
using Sunioj.Options;
using System.Text;

namespace Sunioj.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Register the services needed by the application.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IDbManagerService, DbManagerService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<IServicePackagesService, ServicePackagesService>();
            services.AddScoped<IServiceFaqsService, ServiceFaqsService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IResumesService, ResumesService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IServicePackagesRepository, ServicePackagesRepository>();
            services.AddScoped<IServiceFaqsRepository, ServiceFaqsRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IResumesRepository, ResumesRepository>();
        }

        /// <summary>
        /// Adds JSON Web Token authentication.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="jwtOptions">Jwt Options.</param>
        public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions?.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
