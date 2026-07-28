using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;
using proyectoFeelings.ViewModels;

namespace proyectoFeelings.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ProductController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
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
        [HttpGet]
        public async Task<IActionResult> CreateProduct()

        {
            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;
            var model = new ProductViewModel
            {
               User = currentUser,
               StoreID = storeId ?? 0 // Assuming StoreID is an int, provide a default value if null/

                // Add any other properties your ViewModel contains
            };
            //  return View(currentUser);
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel Product)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;
            var product = new Product
            {
                Code = Convert.ToInt32(Product.Code),
                Description = Product.Description,
                Price = Convert.ToInt32(Product.Price),
                Provider = Product.Provider,
                Status = Product.Status,
                Category = Product.Category,
               
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync(); // Save the product to get the ProductID
            var StoreProduct = new StoreProduct
            {
                ProductID = product.ProductID,
                StoreID = storeId ?? 0, // Assuming StoreID is an int, provide a default value if null
                Quantity = Product.Quantity,
            };
            _context.StoreProduct.Add(StoreProduct);
            await _context.SaveChangesAsync(); // Save the StoreProduct entity to the database
            TempData["SuccessMessage"] = "Producto creado correctamente";
            return RedirectToAction(nameof(ProductList));
        }


        //ProductList
        [HttpGet]
        public async Task<IActionResult> ProductList()

        {
            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;

            var Products = _context.Product
            .Where(p => p.StoreProduct.Any(sp => sp.StoreID == storeId))
        .Select(u => new ProductViewModel
        {
            ProductID = u.ProductID,
            Code = u.Code,
            Description = u.Description,
            Price = u.Price,
            Provider = u.Provider,
            Status = u.Status,
            StoreID = u.StoreProduct.FirstOrDefault().StoreID, // Assuming you want the StoreID from the first StoreProduct
            Category = u.Category,
            Quantity = u.StoreProduct.FirstOrDefault().Quantity,

        })
        .ToList();

            return View(Products);

        }
        //edit product

        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId, int storeId)

        {

           if (productId == null || storeId == null)
            {
              return NotFound();
            }
            var product = await _context.Product.FindAsync(productId);
            var storeProduct = await _context.StoreProduct.FindAsync(productId, storeId);

         if (storeProduct == null)
            {
                return NotFound();

           }
           
            var model = new ProductViewModel
            {
                ProductID = product.ProductID,
                Code = product.Code,
                Description = product.Description,
                Price = product.Price,
                Provider = product.Provider,
                Status = product.Status,
                StoreID = storeProduct.StoreID, // Assuming you want the StoreID from the first StoreProduct
                Category = product.Category,
                Quantity = storeProduct.Quantity,
            };

            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> EditProduct(ProductViewModel model)

        {
           var currentUser = await userManager.GetUserAsync(User);
           var storeId = (currentUser)?.StoreID;

            var product = await _context.Product.FindAsync(model.ProductID);
            var storeProduct = await _context.StoreProduct.FindAsync(model.ProductID, storeId);
          if (product == null || storeProduct == null)
            {
                return NotFound();
            }

            // Update the product properties
            product.Code = Convert.ToInt32(model.Code);
            product.Description = model.Description;
            product.Price = Convert.ToInt32(model.Price);
            product.Provider = model.Provider;
            product.Status = model.Status;
            product.Category = model.Category;

            // Update the storeProduct properties
            storeProduct.Quantity = Convert.ToInt32(model.Quantity);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Producto actualizado correctamente";
            return RedirectToAction(nameof(ProductList));
        }
        //General ProductList
        [HttpGet]
        public async Task<IActionResult> StoresInventory()

        {
            var currentUser = await userManager.GetUserAsync(User);
            var storeId = (currentUser)?.StoreID;

            var Products = _context.Product
            .Where(p => p.StoreProduct.Any(sp => sp.StoreID != storeId))
        .Select(u => new ProductViewModel
        {
            ProductID = u.ProductID,
            Code = u.Code,
            Description = u.Description,
            Price = u.Price,
            Provider = u.Provider,
            Status = u.Status,
            StoreID = u.StoreProduct.FirstOrDefault().StoreID, // Assuming you want the StoreID from the first StoreProduct
            Category = u.Category,
            Quantity = u.StoreProduct.FirstOrDefault().Quantity,

        })
        .ToList();

            return View(Products);

        }
        //Notificacion de productos
        [HttpGet]
        public async Task<IActionResult> ProductNotification()
        {

            var Notifications = _context.Record
            .Where(u => (bool)u.Active)
       .Select(u => new RecordViewModel
       {
           ProductID = u.ProductID,
           Code = u.Product.Code,
           Description = u.Product.Description,
           Provider = u.Product.Provider,
           CurrentStoreID = u.CurrentStoreID, // Assuming you want the StoreID from the first StoreProduct
           Quantity = u.Quantity,
           DateTime = u.DateTime,
           Type = u.Type,
           Comment = u.Comment,

       })
       .ToList();

            return View(Notifications);
        }
        // ack la notificacion
        public async Task<IActionResult> AckNotification(int ProductID, int StoreID, int Type)
        {
            var record = await _context.Record
                .FirstOrDefaultAsync(r =>
                    r.CurrentStoreID == StoreID &&
                    r.Type == Type &&
                    r.Active == true &&
                    r.ProductID == ProductID);
            
            if (record == null)
            {
                return NotFound();
            }
              
            record.Active = false;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Notificacion recibida correctamente";
            return RedirectToAction(nameof(ProductNotification));

        }
        
        

    }
}
