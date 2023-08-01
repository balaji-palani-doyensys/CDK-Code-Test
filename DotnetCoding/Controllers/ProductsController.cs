using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var productDetailsList = await _productService.GetProductByName(name);
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByPriceRange(int minValue, int maxValue)
        {
            var productDetailsList = await _productService.GetProductByPriceRange(minValue, maxValue);
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }



        [HttpGet]
        public async Task<IActionResult> GetProductsByDateRange(DateTime startDate, DateTime endDate)
        {
            var productDetailsList = await _productService.GetProductByPostedOn(startDate, endDate);
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpPost]
        public async Task Create(ProductDetails product)
        {
            _productService.CreateProduct(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, ProductDetails product)
        {
            var productDetailsList = await _productService.Update(Id, product);
            
            return Ok(productDetailsList);
        }

        [HttpDelete]
        public async Task Delete(int Id)
        {
            _productService.DeleteProduct(Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetApprovals()
        {
            var ApprovalList = await _productService.GetApprovals();
            if (ApprovalList == null)
            {
                return NotFound();
            }
            return Ok(ApprovalList);
        }

        [HttpPost]
        public async Task ApproveRejectProduct(int Id, Boolean isApproved)
        { 

        }
    }
}
