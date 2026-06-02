using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class User : IdentityUser

    {

        public string FullName { get; set; }

        [ForeignKey("Store")]
        public int? StoreID { get; set; } 
        public bool AdminAccess { get; set; }
        public bool Status { get; set; }
        public virtual Store Store { get; set; }

        // public string AccessLevel { get; set; }
    }
}
