using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task <IActionResult> EditUser(String Id)
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
            return View(user);
        }
        [HttpGet]
        public IActionResult UserList()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        // post: account/edituser/<userId>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User model)
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
            user.StoreID = model.StoreID;
            user.Status = model.Status;
            user.PhoneNumber = model.PhoneNumber;


            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Console.WriteLine("Usuario actualizado correctamente");
                return RedirectToAction(nameof(UserList));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            Console.WriteLine("Error al actualizar el usuario");
            return View(model);
        }

    }

}
