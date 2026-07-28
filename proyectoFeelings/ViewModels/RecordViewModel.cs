using proyectoFeelings.Models;

namespace proyectoFeelings.ViewModels
{
    public class RecordViewModel
    {
       public int Type { get; set; }
        public int RecordId { get; set; }
        public int Code { get; set; }

        public string Description { get; set; }

        public string Provider { get; set; }

        public int Quantity { get; set; }
        public bool? Active { get; set; }
        public int CurrentStoreID { get; set; }

        public int? NewStoreID { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int ProductID { get; set; }

    }
}
