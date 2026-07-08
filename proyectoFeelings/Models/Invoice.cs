using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("Store")]
        public int StoreID { get; set; }

        [ForeignKey("Product")]

        public int Price { get; set; }

        public DateTime Datetime { get; set; }
        public Store Store { get; set; } // porq el framework lo pide 


        [ForeignKey("Product")]

        public ICollection<StoreProduct> StoreProduct { get; set; }
    }
    
}
