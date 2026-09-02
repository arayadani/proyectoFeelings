using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoFeelings.Data;
using proyectoFeelings.Models;

namespace proyectoFeelings.Services
{
    public class InventoryCheckService
    {
        private readonly AppDbContext _context;

        public InventoryCheckService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CheckInventoryAsync()
        {
            var productsToRestock = await _context.StoreProduct
                .Include(sp => sp.Product)
                .Where(sp => sp.Quantity <= 5)
                .ToListAsync();

            foreach (var item in productsToRestock)
            {
                bool notificationExists = await _context.Record
            .AnyAsync(n =>
                n.ProductID == item.ProductID &&
                n.CurrentStoreID == item.StoreID &&
                n.Active==true
            );

                if (!notificationExists)
                {
                    // aqui se crea la notificacion

                    var Notification1 = new Record
                    {

                        ProductID = item.ProductID,
                        CurrentStoreID = item.StoreID,
                        Quantity = item.Quantity,
                        Active = true,
                        DateTime = DateTime.Now,
                        //   Description = "Producto 1",
                        //   Code = 1,
                        Comment = "ROP",
                        Type = 2,


                    };

                    _context.Record.Add(Notification1);
                    await _context.SaveChangesAsync();

                }
            }
        }
    }
}
