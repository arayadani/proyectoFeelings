using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("Product")]
        //public int ProductID { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public DateTime DateTime { get; set; }

        //[ForeignKey("Store")]
        //public int CurrentStoreID { get; set; }

       // [ForeignKey("Store")]
        public int? NewStoreID { get; set; }

        [ForeignKey("StoreID,ProductID")]
        public StoreProduct StoreProduct { get; set; }


    }
}
