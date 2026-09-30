using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;

namespace proyectoFeelings.Controllers
{
    public class BillingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public BillingController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
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
        // GET: Billing/GeneralBilling


        [HttpGet]
        public IActionResult GeneralBilling()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchValuesAsync(string term)
        {
            /*  string[] values =
               {
           "Apple",
           "Banana",
           "Orange",
           "Pineapple",
           "Banana2",
           "Orange3",
           "Pineapple2"
       };

               var result = values
                   .Where(x => x.Contains(term ?? "", StringComparison.OrdinalIgnoreCase))
                   .Select(x => new
                   {
                       id = x,
                       text = x
                   })
                   .ToList(); */
            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;
            var result = await _context.StoreProduct
                .Where(x => x.StoreID == storeId &&
                            x.Product.Code.ToString().Contains(term ?? ""))
                .Select(x => new
                {
                    id = x.Product.ProductID,
                    description = x.Product.Description,
                    code = x.Product.Code,
                    price = x.Product.Price,
                })
                .Distinct()
                .ToListAsync();

            return Json(result);
        }
    }
}
