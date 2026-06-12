using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;       
using proyectoFeelings.Data;
using proyectoFeelings.Models;

namespace proyectoFeelings.Services
{
    public class SeedServices
    {
        public static async Task SeedDataBase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedServices>>();


            try
            {
                logger.LogInformation("Iniciando la siembra de datos...");
                await context.Database.EnsureCreatedAsync();

                //anadir roles

                logger.LogInformation("Verificando roles...");
                await addRoleAsync(roleManager, "Admin");
                await addRoleAsync(roleManager, "User");
                //anadir store

                var Store1 = new Store
                {
                    StoreName = "Multicentro",
                    PhoneNumber = "2211-3030",
                    Location = "San Jose, Desamparados",
                    Status = true
                };
                context.Store.Add(Store1);
                await context.SaveChangesAsync();

                //anadir usuario admin
                logger.LogInformation("Verificando usuarios...");
                var adminEmail = "admin@gmail.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null) // validacion para no insertar usuario admin si ya existe
                {

                    var adminUser = new User
                    {
                        FullName = "Dani",
                        UserName = adminEmail,
                        Email = adminEmail,
                        SecurityStamp = Guid.NewGuid().ToString(),
                       // StoreID = Store.StoreID,

                    };
                    var result = await userManager.CreateAsync(adminUser, "Dani1234.");

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Usuario admin creado exitosamente.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Error al crear el usuario admin: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));

                    }

                }
                //anadir usuario User
                var userEmail = "user@gmail.com";
                if (await userManager.FindByEmailAsync(userEmail) == null) // validacion para no insertar usuario admin si ya existe
                {
                    var userUser = new User
                    {
                     
                        FullName = "Dani2",
                        UserName = userEmail,
                        Email = userEmail,
                        SecurityStamp = Guid.NewGuid().ToString()

                    };
                    var result = await userManager.CreateAsync(userUser, "Dani1234.");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Usuario de colaborador creado exitosamente.");
                        await userManager.AddToRoleAsync(userUser, "User");
                    }
                    else
                    {
                        logger.LogError("Error al crear el usuario de colaborador: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));

                    }

                }
                var Store = await context.Store.FirstOrDefaultAsync();
                logger.LogInformation("Verificando stores...");
                logger.LogInformation(Store.StoreID.ToString()); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante la siembra de datos: ");

            }
        }

            private static async Task addRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Error al crear el rol {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
                    else
                    {

                    }
            }

            }
    }
}
    
