using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
           _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
           var products = await _repo.GetProductsAsync();
           return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
           var product = await _repo.GetProductByIdAsync(id);
           if(product == null) 
           {
               return NoContent();
           }

           return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrandsAsync()
        {
           var productsBrands = await _repo.GetProductBrandsAsync();
           return Ok(productsBrands);
        }
        
        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypesAsync()
        {
            var productTypes = await _repo.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
}