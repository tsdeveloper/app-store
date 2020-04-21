using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly AccessoIngressoContext _context;
        public ProductsController (AccessoIngressoContext context) {
            _context = context;

        }

        [HttpGet]
        public string GetProducts () {
            return "this will be..";
        }

        [HttpGet ("{id}")]
        public string GetProduct (int id) {
            return "single product";
        }

    }
}