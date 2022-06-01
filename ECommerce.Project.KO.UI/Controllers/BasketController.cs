using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.UI.Dtos;
using ECommerce.Project.KO.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public BasketController(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var result = await _basketService.GetBasket(HttpContext.User.Claims.Where(i => i.Type.Contains("nameidentifier")).FirstOrDefault().Value);

            return View(result);
        }

        public async Task<IActionResult> SaveOrUpdateBasket(long productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product.IsSuccesful)
            {
                BasketDto basketDto = new BasketDto()
                {
                    UserId = HttpContext.User.Claims.Where(i => i.Type.Contains("nameidentifier")).FirstOrDefault().Value,
                    basketItems = new List<BasketItemDto>(){new BasketItemDto() { Quantity = 1, Price = product.Data.Price , ProductId = productId , ProductName = product.Data.ProductName}
                }
                };
                var response = await _basketService.SaveOrUpdate(basketDto);
                return RedirectToAction("GetBasket", "Basket");
            }

            return NotFound();
        }
    }
}
