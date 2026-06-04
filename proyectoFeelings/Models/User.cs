using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class User : IdentityUser

    {
        [Key]
        [NotMapped] // no mapee este campo en la base de datos, porq ya existe 
        public string Id { get; set; }
        public string FullName { get; set; }
        public bool AdminAccess { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Store")]
        public int? StoreID { get; set; }
        public Store Store { get; set; }

    }
}
