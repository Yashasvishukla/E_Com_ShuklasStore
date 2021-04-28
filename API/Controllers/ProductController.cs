using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // Since we have define the type of T we have to create three fields for each of the collections
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepository<Product> productRepo, 
        IGenericRepository<ProductType> productTypeRepo, IGenericRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var spec = new ProductsWithTypeAndBrandSpecification();
            var products = await _productRepo.ListAsync(spec);
            var productToReturnList = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(productToReturnList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var spec = new ProductsWithTypeAndBrandSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            var productToReturn = _mapper.Map<ProductToReturnDto>(product);
            return Ok(productToReturn);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrandsAsync()
        {
            var productBrand = await _productBrandRepo.ListAllAsync();
            return Ok(productBrand);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypesAsync()
        {
            var productType = await _productTypeRepo.ListAllAsync();
            return Ok(productType);
        }
    }
}