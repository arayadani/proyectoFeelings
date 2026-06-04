using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class Invoice
    {
     [Key]
     public int InvoiceId { get; set; }

     [ForeignKey("Product")]
     public int ProductID { get; set; }

     [ForeignKey("Store")]
     public int StoreID { get; set; }

     public int Price { get; set; }
      
     public DateTime Datetime { get; set; }

    }
}
