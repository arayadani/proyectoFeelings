using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            StoreName = u.StoreProduct.FirstOrDefault().Store.StoreName // Assuming you want the StoreName from the first StoreProduct

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
            StoreName = u.StoreProduct.FirstOrDefault().Store.StoreName // Assuming you want the StoreName from the first StoreProduct

        })
        .ToList();

            return View(Products);

        }
        //Notificacion de productos
        [HttpGet]
        public async Task<IActionResult> ProductNotification() // este lista las notificaciones y las muestra en la vista 
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
           NewStoreID = u.NewStoreID,
           Price = u.Product.Price,
           Category = u.Product.Category,

       })
       .ToList();

            return View(Notifications);
        }
        // ack la notificacion
        [HttpGet]
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
        //trasladar producto

        [HttpGet]
        public async Task<IActionResult> MoveProduct(int productID, int storeID)
        {

            var currentUser = await userManager.GetUserAsync(User);
            var userStoreID = (currentUser)?.StoreID;
            var product = await _context.Product.FindAsync(productID);

            var model = new RecordViewModel
            {
                CurrentStoreID = storeID,
                ProductID = (int)product.ProductID,
                NewStoreID = userStoreID,

                Code = product.Code,
                Type = 1, //esto es una operacion de traslado
                Description = product.Description,
                DateTime = DateTime.Now,
            };

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> MoveProduct(RecordViewModel model)// CREA UN RECORD  
        {
            var currentUser = await userManager.GetUserAsync(User);
            var userStoreID = (currentUser)?.StoreID;
            var record = new Record
            {
                ProductID = model.ProductID,
                CurrentStoreID = model.CurrentStoreID,
                NewStoreID = userStoreID ?? 0, // Assuming StoreID is an int, provide a default value if null
                Type = 1, // This is a transfer operation
                Quantity = model.Quantity,
                DateTime = DateTime.Now,
                Active = true,
                Comment = model.Comment
            };
            _context.Record.Add(record);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Trasladado solicitado correctamente";
            return RedirectToAction(nameof(StoresInventory));

        }

        public async Task<IActionResult> ApproveMove(int ProductID, int CurrentStoreID, int NewStoreID, int Quantity, int Code, string Provider, string Description, int Price, string Category)
        {
            var product1 = await _context.Product
           .Include(p => p.StoreProduct)
           .FirstOrDefaultAsync(
          p => p.Code == Code &&
               p.StoreProduct.Any(sp => sp.StoreID == NewStoreID)
      );

            var CurrentStoreProduct = await _context.StoreProduct.FindAsync(ProductID, CurrentStoreID);


            if (product1 == null)
            {
                var product = new Product
                {
                    Code = Code,
                    Description = Description,
                    Price = Price,
                    Provider = Provider,
                    Status = true,
                    Category = Category,
                };
                _context.Product.Add(product);
                await _context.SaveChangesAsync(); // Save the product to get the ProductID
                var StoreProduct = new StoreProduct
                {
                    ProductID = product.ProductID,
                    StoreID = NewStoreID, // Assuming StoreID is an int, provide a default value if null
                    Quantity = Quantity,
                };
                _context.StoreProduct.Add(StoreProduct);
                await _context.SaveChangesAsync(); // Save the product to get the ProductID
            }
            //el producto se encuentra en la tienda de destino, entonces se suma la cantidad
            else
            {
                var NewstoreProduct = await _context.StoreProduct.FindAsync(product1.ProductID, NewStoreID);
                NewstoreProduct.Quantity += Quantity;
            }
                CurrentStoreProduct.Quantity -= Quantity;

            // Update the storeProduct properties
            var record = await _context.Record
                                     .FirstOrDefaultAsync(r =>
                                         r.CurrentStoreID == CurrentStoreID &&
                                         r.NewStoreID == NewStoreID &&
                                         r.Active == true &&
                                         r.Quantity == Quantity &&
                                         r.ProductID == ProductID);

            if (record == null)
            {
                return NotFound();
            }

            record.Active = false; 
            await _context.SaveChangesAsync();
            var record1 = new Record
            {
                ProductID = ProductID,
                CurrentStoreID = CurrentStoreID,
                NewStoreID = NewStoreID,
                Type = 1,
                Quantity = Quantity,
                DateTime = DateTime.Now,
                Active = false,
                Comment = $"Se rebajaron {Quantity} productos"
            };
            _context.Record.Add(record1);
            var record2 = new Record
            {
                ProductID = ProductID,
                CurrentStoreID = CurrentStoreID,
                NewStoreID = NewStoreID,
                Type = 1,
                Quantity = Quantity,
                DateTime = DateTime.Now,
                Active = false,
                Comment = $"Se adicionaron {Quantity} productos"
            };
            _context.Record.Add(record2);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Producto trasladado correctamente";
            return RedirectToAction(nameof(ProductNotification));

        }

        public async Task<IActionResult> RejectMove(int ProductID, int StoreID, int Type)
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
            TempData["SuccessMessage"] = "Traslado declinado correctamente";
            return RedirectToAction(nameof(ProductNotification));

        }
    }
}
