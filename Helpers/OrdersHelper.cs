using SalesCodeSpace.Data;
using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Responses;

namespace SalesCodeSpace.Helpers;

public class OrdersHelper : IOrdersHelper
{
    private readonly DataContext _context;

    public OrdersHelper(DataContext context)
    {
        _context = context;
    }

    public async Task<Response> ProcessOrderAsync(ShowCartViewModel model)
    {
        Response response = await CheckInventoryAsync(model);
        if (!response.IsSuccess)
        {
            return response;
        }

        Sale sale = new()
        {
            Date = DateTime.UtcNow,
            User = model.User,
            Remarks = model.Remarks,
            SaleDetails = new List<SaleDetail>(),
            OrderStatus = OrderStatus.New
        };

        foreach (TemporalSale item in model.TemporalSales)
        {
            sale.SaleDetails.Add(new SaleDetail
            {
                Product = item.Product,
                Quantity = item.Quantity,
                Remarks = item.Remarks,
            });

            Product product = await _context.Products.FindAsync(item.Product.Id);
            if (product != null)
            {
                product.Stock -= item.Quantity;
                _context.Products.Update(product);
            }

            _context.TemporalSales.Remove(item);
        }

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return response;
    }


    private async Task<Response> CheckInventoryAsync(ShowCartViewModel model)
    {
        Response response = new() { IsSuccess = true };
        foreach (TemporalSale item in model.TemporalSales)
        {
            Product product = await _context.Products.FindAsync(item.Product.Id);
            if (product == null)
            {
                response.IsSuccess = false;
                response.Message = $"O produto {item.Product.Name}, já não está disponível";
                return response;
            }
            if (product.Stock < item.Quantity)
            {
                response.IsSuccess = false;
                response.Message = $"Pedimos desculpa mas não temos quantidade suficiente em stock do  {item.Product.Name}. Por favor diminua a quantidade ou substitua por outro.";
                return response;
            }
        }
        return response;
    }
}
