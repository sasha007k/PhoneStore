using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Phones(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var phones = await _phoneService.GetAllItemsAsync();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "BrandDesc" : "";
            ViewData["ModelSortParm"] = sortOrder == "Model" ? "ModelDesc" : "Model";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            switch (sortOrder)
            {
                case "BrandDesc":
                    phones = phones.OrderByDescending(p => p.Brand);
                    break;
                case "Model":
                    phones = phones.OrderBy(p => p.Model);
                    break;
                case "ModelDesc":
                    phones = phones.OrderByDescending(p => p.Model);
                    break;
                case "Price":
                    phones = phones.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    phones = phones.OrderByDescending(p => p.Price);
                    break;
                default:
                    phones = phones.OrderBy(p => p.Brand);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                phones = phones.Where(p => p.Brand.Contains(searchString)
                                       || p.Model.Contains(searchString));
            }


            int pageSize = 4;
            return View(await PaginatedList<PhoneModel>.CreateAsync(phones, pageNumber ?? 1, pageSize));
        }
    }
}