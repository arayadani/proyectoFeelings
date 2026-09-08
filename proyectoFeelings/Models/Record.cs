using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        public int? Quantity { get; set; }
        public int? NewQuantity { get; set; }
        
        public bool? Active { get; set; }

        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public DateTime DateTime { get; set; }

   //     [ForeignKey("Store")]
        public int CurrentStoreID { get; set; }

        public int? NewStoreID { get; set; }

  //      public Store Store { get; set; }
       public Product Product { get; set; }
        public string? Description { get; set; }
        public string? NewDescription { get; set; }

        public int? CurrentPrice { get; set; }
        public int? NewPrice { get; set; }
        public string? Provider { get; set; }

        public string? NewProvider { get; set; }
        public bool? Status { get; set; }
        public bool? NewStatus { get; set; }
        public string? Category { get; set; }

        public string? NewCategory { get; set; }

    }
}
