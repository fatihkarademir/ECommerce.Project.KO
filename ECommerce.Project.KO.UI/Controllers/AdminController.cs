using ECommerce.Project.KO.Business.Abstract;
using ECommerce.Project.KO.Business.DTOs;
using ECommerce.Project.KO.Entities;
using ECommerce.Project.KO.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.KO.UI.Controllers
{
    [Authorize(Roles ="SysAdmin, Admin")]
    public class AdminController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ListProducts()
        {
            var products = _productService.GetAllAsync();

            var result = products.Data.ToList();
            return View(new ProductListModel()
            {
                Products = result
            });

        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            ProductDto newProduct = new ProductDto()
            {
                ProductName = product.Name,
                DiscountRate = product.DiscountRate,
                Description = product.Description,
                Price = product.Price,
            };
            var result = await _productService.CreateAsync(newProduct);
            if (result.IsSuccesful)
            {
                return RedirectToAction("ListProducts");
            }
            ViewBag.ErrorMessage = result.Error;
            return View(product);

        }

        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entityResult = await _productService.GetByIdAsync((int)id);
            if (!entityResult.IsSuccesful)
            {
                return NotFound();
            }
            var entity = entityResult.Data;

            var model = new ProductModel()
            {
                Id = entity.ProductId,
                Name = entity.ProductName,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl
                //SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        //[Route("/admin/products/{}")]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = await _productService.GetByIdAsync(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Data.ProductName = model.Name;
                entity.Data.Description = model.Description;
                //entity.ImageUrl = model.ImageUrl;
                entity.Data.Price = model.Price;
                if (file != null)
                {
                    entity.Data.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                await _productService.UpdateAsync(entity.Data, model.Id);

                return RedirectToAction("ListProducts");
            }
            ViewBag.Categories = _categoryService.GetAllAsync();
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var entity = await _productService.GetByIdAsync(productId);
            if (entity == null)
            {
                NotFound();
            }
            await _productService.DeleteAsync(productId);
            return RedirectToAction("ListProducts");
        }

        //---------------------------Category Section-----------------------------------

        public IActionResult ListCategories()
        {
            var categories = _categoryService.GetAllAsync();

            var result = categories.Data.ToList();
            return View(new CategoryListModel()
            {
                Categories = result
            });
        }

        [Authorize(Roles ="SysAdmin")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            CategoryDto newCategory = new CategoryDto()
            {
                CategoryName = model.Name,
            };
            await _categoryService.CreateAsync(newCategory);
            return RedirectToAction("ListCategories");
        }
    }
}
