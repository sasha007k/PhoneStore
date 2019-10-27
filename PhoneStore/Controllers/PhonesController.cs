using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using PhoneStore.Services;

namespace PhoneStore.Controllers
{
    public class PhonesController : Controller
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Phones(int page=1)
        {
            var phones = _phoneService.GetAllItems(page);
            
            return View(phones);
        }


        public async Task<IActionResult> AddPhone(PhoneModel newPhone)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Phones");
            }

            if (string.IsNullOrWhiteSpace(newPhone.Brand) || string.IsNullOrWhiteSpace(newPhone.Model))
            {
                return RedirectToAction("Phones");
            }

            var successful = await _phoneService.AddPhoneAsync(newPhone);
            if (!successful)
            {
                return BadRequest("Could not add phone.");
            }

            return RedirectToAction("Phones");
        }

        public async Task<IActionResult> DeletePhone(int id)
        {
            var successful = await _phoneService.DeletePhoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not delete phone.");
            }

            return RedirectToAction("Phones");
        }

        [HttpPost]
        public async Task<IActionResult> AddSale(int id, double sale)
        {
            var successful = await _phoneService.AddSaleAsync(id, sale);
            if (!successful)
            {
                return BadRequest("Could not create sale.");
            }

            return RedirectToAction("Phones");
        }

        [Authorize]
        public async Task<IActionResult> AddPhoneToShoppingCart(int id)
        {
            var successful = await _phoneService.AddPhoneToShoppingCartAsync(id);
            if (!successful)
            {
                return BadRequest("Could not add phone to busket.");
            }

            return RedirectToAction("Phones");
        }
    }
}