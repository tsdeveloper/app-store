using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult> GetProducts () {
            var products = await _productRepository.GetProductAllAsync ();
            return Ok (products);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult> GetProduct (Guid id) {
            var products = await _productRepository.GetProductByIdAsync (x => x.Id.Equals(id));
            return Ok (products);
        }

    }
}