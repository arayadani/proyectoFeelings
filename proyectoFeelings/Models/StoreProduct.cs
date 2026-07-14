using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoFeelings.Models
{
    [PrimaryKey(nameof(StoreID), nameof(ProductID))] // storeid y productid son la clave primaria compuesta
    public class StoreProduct

    {
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [ForeignKey("Store")]
        public int StoreID { get; set; }
        public int Quantity { get; set; }

        public Store Store { get; set; }
        public Product Product { get; set; }


    }
}
