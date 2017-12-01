using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminRole = RoleConstants.Admin;
                        var trainerRole = RoleConstants.Trainer;
                        var blogAuthorRole = RoleConstants.BlogAuthor;
                        var adminRoleExists = await roleManager.RoleExistsAsync(adminRole);
                        var trainerRoleExists = await roleManager.RoleExistsAsync(trainerRole);
                        var blogAuthorRoleExists = await roleManager.RoleExistsAsync(blogAuthorRole);
                        if (!adminRoleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = adminRole
                            });
                        }
                        if (!trainerRoleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = trainerRole
                            });
                        }
                        if (!blogAuthorRoleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = blogAuthorRole
                            });
                        }

                        var adminUser = await userManager.FindByEmailAsync("admin@abv.bg");

                        var user = new User
                        {
                            Name = "Pesho",
                            Email = "admin@abv.bg",
                            UserName = "admin@abv.bg",
                        };

                        if (adminUser == null)
                        {
                            await userManager.CreateAsync(user
                            , "123");

                            await userManager.AddToRoleAsync(user, adminRole);
                        }
                    })
                    .Wait();
            }
            return app;
        }
    }
}
