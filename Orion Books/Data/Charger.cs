using Microsoft.AspNetCore.Identity;
using Orion_Books.Models;
using System.Net;

namespace Orion_Books.Data
{
    public class Charger
    {
        public static async Task ChargeUsers(IApplicationBuilder applicationBuilder)
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
                string adminUserEmail = "wiilfern1910@outlook.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Usuario()
                    {
                        UserName = "hotwillas",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Rua = "Rua 14 de Julho",
                            Bairro = "Capelacarai",
                            Cidade = "Vinhedo",
                            Numero = 193
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "12345Koala@?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.adminUser);
                }

                string UsuarioEmail = "marcelogoulart@gmail.com";

                var Usuario = await userManager.FindByEmailAsync(UsuarioEmail);
                if (Usuario == null)
                {
                    var newUsuario = new Usuario()
                    {
                        UserName = "marcelo_goulart",
                        Email = UsuarioEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Rua = "Rua Campinas",
                            Bairro = "Campinas",
                            Cidade = "Campinas",
                            Numero = 192
                        }
                    };
                    await userManager.CreateAsync(newUsuario, "12345Abacate@?");
                    await userManager.AddToRoleAsync(newUsuario, UserRoles.standardUser);
                }
            }
        }
    }
}
