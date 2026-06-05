using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<StoreProduct> StoreProduct { get; set; }



    }
}
