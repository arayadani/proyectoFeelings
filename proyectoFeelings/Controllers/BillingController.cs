using Microsoft.AspNetCore.Mvc;

namespace proyectoFeelings.Controllers
{
    public class BillingController : Controller
    {
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
        public IActionResult SearchValues(string term)
        {
            string[] values =
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
                .ToList();

            return Json(result);
        }
    }
}
