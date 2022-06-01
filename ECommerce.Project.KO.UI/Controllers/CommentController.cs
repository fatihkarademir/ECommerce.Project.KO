using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        IProductService _productService;
        ICommentService _commentService;
        private UserManager<IdentityUser> _userManager;

        public CommentController(IProductService productService, ICommentService commentService, UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(ProductDetailsModel productDetailsModel)
        {
            var user =await _userManager.FindByIdAsync(HttpContext.User.Claims.Where(i=> i.Type.Contains("nameidentifier")).FirstOrDefault().Value);
            productDetailsModel.AddedComment.ProductId = productDetailsModel.ProductId;
            productDetailsModel.AddedComment.UserId = user.Id;
            await _commentService.CreateAsync(productDetailsModel.AddedComment);
            return RedirectToAction("List", "Shop");
        }
    }
}
