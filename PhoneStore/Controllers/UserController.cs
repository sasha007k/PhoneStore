using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Data;
using PhoneStore.Models;
using PhoneStore.Models.Display;
using PhoneStore.Services;

namespace PhoneStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        public async Task<IActionResult> GetShoppingCart()
        {
            var shoppingCartDisplay = await _userService.GetShoppingCartAsync();

            return View(shoppingCartDisplay);
        }
        public async Task<IActionResult> CancelPhone(int id)
        {
            var successful = await _userService.CancelPhoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not cancel this phone.");
            }

            return RedirectToAction("GetShoppingCart", "User");
        }
        public async Task<IActionResult> CreateOrder(string address)
        {
            var successful = await _userService.CreateOrderAsync(address);
            if (!successful)
            {
                return BadRequest("Could not create this order.");
            }

            return RedirectToAction("GetShoppingCart", "User");
        }
        [Authorize]
        public async Task<IActionResult> History()
        {
            var orders = await _userService.GetHistoryAsync();
            return View(orders);
        }
    }
}