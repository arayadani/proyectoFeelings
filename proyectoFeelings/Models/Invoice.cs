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

        public DateTime Datetime { get; set; }

        public decimal Total { get; set; }

        // Navigation property
        public Store Store { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
            = new List<InvoiceDetail>();

    }
    
}
