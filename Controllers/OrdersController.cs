using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCodeSpace.Data;
using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Helpers;
using Vereyon.Web;

namespace SalesCodeSpace.Controllers;

public class OrdersController : Controller
{
    private readonly DataContext _context;
    private readonly IFlashMessage _flashMessage;
    private readonly IOrdersHelper _ordersHelper;

    public OrdersController(DataContext context,
    IFlashMessage flashMessage, IOrdersHelper ordersHelper)
    {
        _context = context;
        _flashMessage = flashMessage;
        _ordersHelper = ordersHelper;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Sales
            .Include(s => s.User)
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.Product)
            .ToListAsync());
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales
            .Include(s => s.User)
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.Product)
            .ThenInclude(p => p.ProductImages)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Dispatch(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        if (sale.OrderStatus != OrderStatus.New)
        {
            _flashMessage.Danger("Só se pode despachar pedidos em estado 'Novo'.");
        }
        else
        {
            sale.OrderStatus = OrderStatus.Dispatch;
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("O estado do pedido foi alterado para 'Despachado'.");
        }

        return RedirectToAction(nameof(Details), new { Id = sale.Id });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Send(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        if (sale.OrderStatus != OrderStatus.Dispatch)
        {
            _flashMessage.Danger("Só pode enviar pedidos em estado 'Despachado'.");
        }
        else
        {
            sale.OrderStatus = OrderStatus.Send;
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("O estado do pedido foi alterado para 'Enviado'.");
        }

        return RedirectToAction(nameof(Details), new { Id = sale.Id });
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Confirm(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        if (sale.OrderStatus != OrderStatus.Send)
        {
            _flashMessage.Danger("Só se podem confirmar pedidos que estejam no estado 'Enviado'.");
        }
        else
        {
            sale.OrderStatus = OrderStatus.Confirm;
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("O estado do pedido foi alterado para 'Confirmado'.");
        }

        return RedirectToAction(nameof(Details), new { Id = sale.Id });
    }


    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Cancel(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        if (sale.OrderStatus == OrderStatus.Canceled)
        {
            _flashMessage.Danger("Não pode cancelar um pedido que já estava 'Cancelado'.");
        }
        else
        {
            await _ordersHelper.CancelOrderAsync(sale.Id);
            _flashMessage.Confirmation("O estado do pedido foi alterado para 'Cancelado'.");
        }

        return RedirectToAction(nameof(Details), new { Id = sale.Id });
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> MyOrders()
    {
        return View(await _context.Sales
            .Include(s => s.User)
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.Product)
            .Where(s => s.User.UserName == User.Identity.Name)
            .ToListAsync());
    }

    [Authorize(Roles = "User")]
    public async Task<IActionResult> MyDetails(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Sale sale = await _context.Sales
            .Include(s => s.User)
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.Product)
            .ThenInclude(p => p.ProductImages)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

}

