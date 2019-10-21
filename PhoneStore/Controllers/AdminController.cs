using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Services;

namespace PhoneStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult GetOrders()
        {
            return View(_adminService.GetOrdersAsync());
        }

        //public async Task<IActionResult> GetOrderDetails(int id)
        //{
        //    Order order = await this._context.Orders.FindAsync(id);

        //    User user = (User)await this._manager.FindByIdAsync(order.UserId);

        //    GetOrderDetailsDto dto = new GetOrderDetailsDto()
        //    {
        //        Email = user.Email
        //    };

        //    var books = (from i in this._context.Books
        //                 where i.OrderId == order.Id
        //                 select new BookDisplay()
        //                 {
        //                     Id = i.Id,
        //                     Name = i.Name,
        //                     Author = i.Author,
        //                     Price = i.Price
        //                 }).ToList<BookDisplay>();

        //    double totalsum = books.Sum(b => b.Price);

        //    dto.Books = books;
        //    dto.TotalSum = totalsum;

        //    return View(dto);
        //}

        public async Task<IActionResult> OpenOrder(int id)
        {
           await _adminService.OpenOrderAsync(id);

            return RedirectToAction("GetOrders", "Admin");
        }

        public async Task<IActionResult> CloseOrder(int id)
        {
            await _adminService.CloseOrderAsync(id);

            return RedirectToAction("GetOrders", "Admin");
        }
    }
}