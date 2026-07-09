using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace proyectoFeelings.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
        

            return View();
        }

        [Authorize] // solo los usuarios autenticados pueden acceder a esta acción
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult User()
        {
            return View();
        }



    }
}
