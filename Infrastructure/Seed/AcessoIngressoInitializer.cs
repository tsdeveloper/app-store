using System;
using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Bogus;

namespace Infrastructure.Seed
{
    public class AcessoIngressoInitializer
    {
        private readonly AcessoIngressoContext _context;
        public AcessoIngressoInitializer(AcessoIngressoContext context)
        {
            _context = context;
          
        }

        private void FactoryProducts()
        {
            if (!_context.Products.Any()){
                var category = new Faker<Product>()
                    .RuleFor(p => p.ProductId, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.Department())
                    .Generate(4);
                _context.Products.AddRange(category);
                _context.SaveChanges();
            }
    }

        public void SeedDemo()
        {
            FactoryProducts();
        }
    }
}