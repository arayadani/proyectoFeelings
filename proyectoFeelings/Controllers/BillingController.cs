using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;
using System.Text.Json;

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
        public async Task<IActionResult> InvoiceCreationAsync([FromBody] JsonDocument request)
        {
            var root = request.RootElement;

            int total = root.GetProperty("total").GetInt32();

            var productos = root.GetProperty("productos");

            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;
            if (storeId == null)
            {
                return NotFound();
            }

            //Console.WriteLine("total3:" + total);

            var invoice = new Invoice
            {
                StoreID = (int)storeId,
                Datetime = DateTime.Now,
                Total = total,
            };

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            var Invoice = await _context.Invoice
                .OrderByDescending(i => i.Datetime)
                .FirstOrDefaultAsync();


            foreach (var item in productos.EnumerateArray())
            {
                string codigo = item.GetProperty("codigo").ToString();
                string producto = item.GetProperty("producto").GetString() ?? "";

                int cantidad = item.GetProperty("cantidad").GetInt32();

                int precio = item.GetProperty("precio").GetInt32();
                int subtotal = item.GetProperty("subtotal").GetInt32();

                var product = await _context.Product.FirstOrDefaultAsync(p => p.Code.ToString() == codigo && p.Description == producto);

                var invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = Invoice.InvoiceId,
                    ProductID = product.ProductID,
                    Quantity = cantidad,
                    Price = precio,
                    Subtotal = subtotal,
                    Invoice = Invoice,
                    Product = product,
                };

                _context.InvoiceDetail.Add(invoiceDetail);
                await _context.SaveChangesAsync();

                var record2 = new Record
                {
                    ProductID = invoiceDetail.ProductID,
                    CurrentStoreID = (int)storeId,
                    Type = 6,
                    Total = subtotal,
                    InvoiceId = invoice.InvoiceId,
                    DateTime = DateTime.Now,
                    Active = false,
                    Author = currentUser.FullName,
                    Quantity = cantidad,
                    Comment = $"Rebajo por factura: {Invoice.InvoiceId}"
                };
                _context.Record.Add(record2);
                await _context.SaveChangesAsync();


                //logica de rebajo de products

            }


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
