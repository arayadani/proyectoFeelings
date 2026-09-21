using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string FullName { get; set; }
        public string? NewFullName { get; set; }

        public string Email { get; set; }
        public string? NewEmail { get; set; }

        public string Password { get; set; }

        public int StoreId { get; set; }
        public int? NewStoreId { get; set; }

        public bool AdminAccess { get; set; }
        public bool? NewAdminAccess { get; set; }

        public bool Status { get; set; }
        public bool? NewStatus { get; set; }

        public string PhoneNumber { get; set; }
        public string? NewPhoneNumber { get; set; }

        public string? NewPassword { get; set; }
        public string StoreName { get; set; }
        public string? ConfirmPassword { get; set; }



        public List<SelectListItem> Stores { get; set; }
    }
}
