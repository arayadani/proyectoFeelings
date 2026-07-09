using Microsoft.AspNetCore.SignalR;
using proyectoFeelings.Models;

namespace proyectoFeelings.ViewModels
{
    public class ProductViewModel
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string? Provider { get; set; }
        public bool Status { get; set; }
        public string Category { get; set; }

        public int Quantity { get; set; }

        public int StoreID { get; set; }

        public User User { get; set; }

        
    }
}
