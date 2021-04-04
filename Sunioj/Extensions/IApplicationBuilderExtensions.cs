using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sunioj.Core.Contracts.Services;

namespace Sunioj.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetService<IDbManagerService>();

            dbInitializer.Migrate();

            if (env.IsDevelopment())
            {
                dbInitializer.Seed();
            }
        }
    }
}
