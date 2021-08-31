using Microsoft.AspNetCore.Mvc;
using Domain.Models.Products;
using Shop.Web.Models.Products;
using Application.Services.Fondations.FileManager;
using Application.Services.Fondations.Products;
using System;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IFileManager fileManager;
        IProductService productService;

        public ProductController(IFileManager fileManager, IProductService productService)
        {
            this.fileManager = fileManager;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel ProductViewModel)
        {
            try
            {
                Product product = MapProductDTO(ProductViewModel);
                productService.AddProductAsync(product);
            }
            catch { }
            return View(ProductViewModel);
        }

        private Product MapProductDTO(ProductViewModel ProductViewModel)
        {
            return new Product
            {
                ProductId = Guid.NewGuid(),
                Name = ProductViewModel.Name,
                Price = ProductViewModel.Price,
                ShortDescription = ProductViewModel.ShortDescription,
                FullDescription = ProductViewModel.FullDescription,
                Sku = ProductViewModel.Sku,
                StockQuantity = ProductViewModel.StockQuantity,
                ShowOnHomepage = ProductViewModel.ShowOnHomePage,
                Picture = fileManager.SaveImage(ProductViewModel.ImageFile)
            };
        }
    }
}
