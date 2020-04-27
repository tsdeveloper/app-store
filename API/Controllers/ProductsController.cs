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
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;


        public ProductsController (IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts () {
            var products = await _productRepo.GetTAllAsync (filter: x => x.IsCanceled.Equals(false));
            return Ok (products);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult> GetProduct (Guid id) {
            var products = await _productRepo.GetByIdAsync (id: id, filter: x => x.IsCanceled.Equals(false));
            return Ok (products);
        }
        
        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrand () {
            var productsBrands = await _productBrandRepo.GetTAllAsync(filter: x => x.IsCanceled.Equals(false));
            return Ok (productsBrands);
        }
        
        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsType () {
            var productsType = await _productTypeRepo.GetTAllAsync(filter: x => x.IsCanceled.Equals(false));
            return Ok (productsType);
        }

    }
}