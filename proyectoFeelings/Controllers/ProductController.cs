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
        public IActionResult ProductList()

        {
            var Products = _context.Product
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
        public async Task<IActionResult> EditProduct(String productId, String storeId)

        {
            var ProductId = Convert.ToInt32(productId);
            var StoreId = Convert.ToInt32(storeId);


            if (ProductId == null || StoreId == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(ProductId);
            var storeProduct = await _context.StoreProduct.FindAsync(ProductId, StoreId);

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

    }
}
