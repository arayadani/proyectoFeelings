using Microsoft.AspNetCore.Identity;

namespace proyectoFeelings.Models
{
    public class Users : IdentityUser

    {
        public string FullName { get; set; }
        public string AccessLevel { get; set; }
    }
}
