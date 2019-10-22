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

        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var orders = await _adminService.GetOrderDetailsAsync(id);
            return View(orders);
        }

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