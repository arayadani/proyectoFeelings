using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;
using proyectoFeelings.ViewModels;

namespace proyectoFeelings.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {

            this._context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            
               var user = await userManager.FindByEmailAsync(model.Email);
               if (user == null)
               {

                   return NotFound();

               }
               if (!user.Status)
               {
                   return RedirectToAction("InactiveUser");

               }
           
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Error al iniciar sesion");
            return View(model);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        // get: account/edituser/<userId>
        public async Task<IActionResult> EditUser(String Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();

            }
            //   return View(user);
            var model = new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AdminAccess = user.AdminAccess,
                Status = user.Status,
                StoreId = (int)user.StoreID,
                // Add any other properties your ViewModel contains
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult UserList()
        {
            var users = _context.Users
        .Select(u => new UserViewModel
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Status = u.Status,
            AdminAccess = u.AdminAccess,
            StoreId = (int)u.StoreID,

        })
        .ToList();

            return View(users);
            //var users = _context.Users.ToList();
            //  return View(users);
        }
        // post: account/edituser/<userId>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            //  if (!ModelState.IsValid)
            // {
            // Console.WriteLine("ModelState is not valid");
            // return View(model);

            //  }
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                Console.WriteLine("Usuario no encontrado");
                return NotFound();

            }

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email; // si el username es un correo
            user.AdminAccess = model.AdminAccess;
            user.StoreID = model.StoreId;
            user.Status = model.Status;
            user.PhoneNumber = model.PhoneNumber;


            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Console.WriteLine("Usuario actualizado correctamente");
                var currentRole = (await userManager.GetRolesAsync(user))
                    .FirstOrDefault();
                //   Console.WriteLine($"Rol actual del usuario: {currentRole}");

                if (model.AdminAccess == true)
                {
                    if (!string.IsNullOrEmpty(currentRole))
                    {
                        await userManager.RemoveFromRoleAsync(user, currentRole);
                    }

                    await userManager.AddToRoleAsync(user, "Admin");

                }
                else
                {
                    if (!string.IsNullOrEmpty(currentRole))
                    {
                        await userManager.RemoveFromRoleAsync(user, currentRole);
                    }
                    await userManager.AddToRoleAsync(user, "User");
                }
                return RedirectToAction(nameof(UserList));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            Console.WriteLine("Error al actualizar el usuario");
            return View(model);


        }
        // get: account/createuser
        public async Task<IActionResult> CreateUser()
        {
            var model = new UserViewModel();

            model.Stores = await _context.Store
                .Select(s => new SelectListItem
                {
                    Value = s.StoreID.ToString(),
                    Text = s.StoreName
                })
                .ToListAsync();

            return View(model);
        }
        // post: account/createuser

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (await userManager.FindByEmailAsync(model.Email) == null)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    AdminAccess = model.AdminAccess,
                    StoreID = model.StoreId,
                    PhoneNumber = model.PhoneNumber,
                    Status = model.Status,

                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.AdminAccess)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                    TempData["SuccessMessage"] = "Usuario creado correctamente";
                    return RedirectToAction(nameof(UserList));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ViewBag.Stores = await _context.Store.ToListAsync();
                TempData["ErrorMessage"] = "Error al crear el usuario";

                return RedirectToAction(nameof(UserList));


            }
            else
            {

                TempData["ErrorMessage"] = "Error al crear el usuario, el correo ya existe";
                return RedirectToAction(nameof(UserList));

            }
        }
        [HttpGet]
        public IActionResult InactiveUser()
        {
            return View();
        }
    } 

    }
