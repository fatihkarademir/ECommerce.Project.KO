using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private ICommentService _commentService;
        private UserManager<IdentityUser> _userManager;

        public ShopController(ICommentService commentService, IProductService productService, UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productResult = await _productService.GetDetailByIdAsync((int)id);
            if (!productResult.IsSuccesful)
            {
                return NotFound();
            }
            var model = new ProductDetailsModel()
            {
                Product = productResult.Data,
                Category = productResult.Data.Category
            };
            var result = await _commentService.GetCommentsByProductId((int)id);

            if (result.Data.Any())
            {                
                model.AllComments = result.Data;
                foreach (var item in model.AllComments)
                {
                    item.UserDto = await _userManager.FindByIdAsync(item.UserId);                   
                }
            }

            //Burada direkt produt'ı göndermek yerine model oluşturup göndereceğiz.
            //return View(product);
            return View(model);
        }

        public IActionResult List()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAllAsync().Data.ToList()
            });
        }
    }
}
