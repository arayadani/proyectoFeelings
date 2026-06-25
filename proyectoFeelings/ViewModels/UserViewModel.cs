using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int StoreId { get; set; }
        public bool AdminAccess { get; set; }
        public bool Status { get; set; }
        public string PhoneNumber { get; set; }



        public List<SelectListItem> Stores { get; set; }
    }
}
