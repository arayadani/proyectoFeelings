using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proyectoFeelings.Models;
using System.Diagnostics;

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
