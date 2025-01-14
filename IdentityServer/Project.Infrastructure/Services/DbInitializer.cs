using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Project.Domain.IdentityEntities;
using Project.Persistence;

namespace Project.Infrastructure.Services
{
    public class DbInitializer(UserManager<ProjectUser> userManager, RoleManager<IdentityRole> roleManager)
        : IDbInitializer
    {

        public async Task Start()
        {
            await AdminSeeder();
            await CustomerSeeder();
        }

        private async Task AdminSeeder()
        {
            IdentityResult result = null;
            var adminEntity = await roleManager.FindByNameAsync(ProjectIdentityItems.AdminRole);
            if (adminEntity is not null)
            {
                return;
            }
            result = await roleManager.CreateAsync(new IdentityRole(ProjectIdentityItems.AdminRole));
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create AdminRole. {ParseError(result.Errors)}");
            }

            ProjectUser adminUser = new()
            {
                UserName = "admin1@project.com",
                Email = "admin1@project.com",
                EmailConfirmed = true,
                PhoneNumber = "232323232323",
                NormalizedUserName = "Neil Armstrong",
            };

            result = await userManager.CreateAsync(adminUser, "Super");
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create Admin. {ParseError(result.Errors)}");
            }
            result = await userManager.AddToRoleAsync(adminUser, ProjectIdentityItems.AdminRole);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't addd Admin to Role. {ParseError(result.Errors)}");
            }

            result = await userManager.AddClaimsAsync(
                adminUser,
                [
                    new(JwtClaimTypes.Name, adminUser.NormalizedUserName),
                    new(JwtClaimTypes.Role, ProjectIdentityItems.AdminRole)
                ]);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create Admin Claim. {ParseError(result.Errors)}");
            }
        }

        private async Task CustomerSeeder()
        {
            IdentityResult result = null;
            var customerEntity = await roleManager.FindByNameAsync(ProjectIdentityItems.CustomerRole);
            if (customerEntity is not null)
            {
                return;
            }

            result = await roleManager.CreateAsync(new IdentityRole(ProjectIdentityItems.CustomerRole));
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create CustomerRole. {ParseError(result.Errors)}");
            }

            ProjectUser customerUser = new()
            {
                UserName = "customer1@project.com",
                Email = "customer1@project.com",
                EmailConfirmed = true,
                PhoneNumber = "69696969696969",
                NormalizedUserName = "Chris Hadfield",
            };

            result = await userManager.CreateAsync(customerUser, "Medium");
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create Customer. {ParseError(result.Errors)}");
            }
            result = await userManager.AddToRoleAsync(customerUser, ProjectIdentityItems.CustomerRole);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't addd Customer to Role. {ParseError(result.Errors)}");
            }

            result = await userManager.AddClaimsAsync(
                customerUser,
                [
                    new(JwtClaimTypes.Name, customerUser.NormalizedUserName),
                    new(JwtClaimTypes.Role, ProjectIdentityItems.CustomerRole)
                ]);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create Customer Claim. {ParseError(result.Errors)}");
            }
        }

        private static string ParseError(IEnumerable<IdentityError> errors) =>
            $"{string.Join($"{Environment.NewLine}", errors.Select(err => $"{err.Code} - {err.Description}"))}";
    }
}
