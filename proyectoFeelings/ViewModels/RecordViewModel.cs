using proyectoFeelings.Models;

namespace proyectoFeelings.ViewModels
{
    public class RecordViewModel
    {
        string Type { get; set; }

        int StoreId { get; set; }

        int Code { get; set; }

        string Description { get; set; }

        string Provider { get; set; }

        int Quantity { get; set; }
        
    }
}
