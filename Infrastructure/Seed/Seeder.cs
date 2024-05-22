using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
{
    public async Task SeedUser()
    {
        try
        {
            var existing = await userManager.FindByNameAsync("admin");
            if (existing != null) return;

            var user = new IdentityUser()
            {
                Email = "admin@gmail.com",
                PhoneNumber = "+992988149811",
                UserName = "admin"
            };

           var res= await userManager.CreateAsync(user, "11111");
           await userManager.AddToRoleAsync(user, Roles.Admin);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public async Task SeedRole()
    {
        try
        {
            var newRoles = new List<IdentityRole>()
            {
                new(Roles.Admin),
                new(Roles.User),
                new(Roles.Trainer)
            };

            var existing = roleManager.Roles.ToList();
            foreach (var role in newRoles)
            {
                if (existing.Exists(e => e.Name == role.Name) == false)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}