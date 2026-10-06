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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoiceCreationAsync([FromBody] int total)
        {
            // Console.WriteLine("HOLI MUNDO");

            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;
            if (storeId == null)
            {
                return NotFound();
            }


            Console.WriteLine("total3:" + total);

            var invoice = new Invoice
            {
                StoreID = (int)storeId,
                Datetime = DateTime.Now,
                Total = total,
            };

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();
            var Invoice = await _context.Invoice
           .FirstOrDefaultAsync();

            var record = new Record
            {
                CurrentStoreID = (int)storeId,
                Type = 6,
                Total = total,
                InvoiceId = Invoice.InvoiceId,
                DateTime = DateTime.Now,
                Active = false,
                Author = currentUser.FullName,
                Comment = $"Se genero una factura."
            };
            _context.Record.Add(record);
            await _context.SaveChangesAsync();
       //     TempData["SuccessMessage"] = "Trasladado solicitado correctamente";
            //   return RedirectToAction(nameof(GeneralBilling));
            return Json(new
            {
                success = true,
                message = "Factura generada correctamente desde el controller."
            });

        }
    }

}
