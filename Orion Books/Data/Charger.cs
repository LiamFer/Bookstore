using Microsoft.AspNetCore.Identity;
using Orion_Books.Models;
using System.Net;

namespace Orion_Books.Data
{
    public class Charger
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.adminUser))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.adminUser));
                if (!await roleManager.RoleExistsAsync(UserRoles.standardUser))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.standardUser));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Usuario()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Rua = "123 Main St",
                            Bairro = "Charlotte",
                            Cidade = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.adminUser);
                }

                string UsuarioEmail = "user@etickets.com";

                var Usuario = await userManager.FindByEmailAsync(UsuarioEmail);
                if (Usuario == null)
                {
                    var newUsuario = new Usuario()
                    {
                        UserName = "app-user",
                        Email = UsuarioEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Rua = "123 Main St",
                            Bairro = "Charlotte",
                            Cidade = "NC"
                        }
                    };
                    await userManager.CreateAsync(newUsuario, "Coding@1234?");
                    await userManager.AddToRoleAsync(newUsuario, UserRoles.standardUser);
                }
            }
        }
    }
}
