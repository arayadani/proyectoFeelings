using System.ComponentModel.DataAnnotations;

namespace proyectoFeelings.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string? Provider { get; set; }
        public bool Status { get; set; }
        public string Category { get; set; }
        public ICollection<StoreProduct> StoreProduct { get; set; }  


    }
}
