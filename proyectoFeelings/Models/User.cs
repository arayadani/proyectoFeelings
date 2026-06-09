using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class User : IdentityUser

    {
        //  [Key]
        //  public string Id { get; set; }
        //estan para mi referencia para no olvidar que el Id ya viene incluido en IdentityUser
        public string FullName { get; set; }
        public bool AdminAccess { get; set; }
        public bool Status { get; set; }

        [ForeignKey("Store")]
        public int? StoreID { get; set; }
        public Store Store { get; set; }

    }
}
