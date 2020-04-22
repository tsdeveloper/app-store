using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly AcessoIngressoContext _context;
        public ProductsController (AcessoIngressoContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts () 
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet ("{id}")]
        public string GetProduct (int id) {
            return "single product";
        }

    }
}