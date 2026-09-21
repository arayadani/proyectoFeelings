using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;
using proyectoFeelings.ViewModels;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;


namespace proyectoFeelings.Controllers
{
    public class StoreController : Controller
    {

        private readonly AppDbContext _context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public StoreController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {

            this._context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //create store
        [HttpGet]
        public async Task<IActionResult> CreateStore()

        {
           
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateStore(Store model) //model es el tipo y store la variable que recibe los datos del formulario
        {
            var store = new Store
            {
                StoreName = model.StoreName,
                Location = model.Location,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status
            };
            _context.Store.Add(store);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Tienda creada correctamente";
            return RedirectToAction(nameof(StoreList));
        }
        

        //ProductList

        [HttpGet]
        public async Task<IActionResult> StoreList()

        {
        

            var Stores = _context.Store
        .Select(u => new Store
        {
            StoreID = u.StoreID,
            StoreName = u.StoreName,
            Location = u.Location,
            PhoneNumber = u.PhoneNumber,
            Status = u.Status
        })

       
        .ToList();

            return View(Stores);

        }

        [HttpGet]
        public async Task<IActionResult> EditStore(int storeId)

        {

            if (storeId == null)
            {
                return NotFound();
            }
            var store = await _context.Store.FindAsync( storeId);

            if (store == null)
            {
                return NotFound();

            }

            var model = new Store
            {
                StoreID = store.StoreID,
                StoreName = store.StoreName,
                Location = store.Location,
                PhoneNumber = store.PhoneNumber,
                Status = store.Status
            };
          

            return View(model);

        }
      [HttpPost]
         public async Task<IActionResult> EditStore(Store model)

        {
            var store = await _context.Store.FindAsync(model.StoreID);

            if (store == null)
            {
                return NotFound();
            }

            store.StoreName = model.StoreName;
            store.Location = model.Location;
            store.PhoneNumber = model.PhoneNumber;
            store.Status = model.Status;

            // Update the storeProduct properties
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Tienda actualizada correctamente";
            return RedirectToAction(nameof(StoreList));
        }
    }
}
