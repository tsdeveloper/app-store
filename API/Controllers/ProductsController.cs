using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly IProductRepository _productRepository;

        public ProductsController (IProductRepository productRepository) {
            _productRepository = productRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts () {
            var products = await _productRepository.GetProductAllAsync ();
            return Ok (products);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult> GetProduct (Guid id) {
            var products = await _productRepository.GetProductByIdAsync (x => x.Id.Equals(id));
            return Ok (products);
        }
        
        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrand () {
            var productsBrands = await _productRepository.GetProductBrandAllAsync();
            return Ok (productsBrands);
        }
        
        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsType () {
            var productsType = await _productRepository.GetProductTypeAllAsync();
            return Ok (productsType);
        }

    }
}