using System;
using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Bogus;
using System.Collections.Generic;

namespace Infrastructure.Seed
{
    public class AcessoIngressoInitializer
    {
        private readonly AcessoIngressoContext _context;
        private List<Product> fakerProductList = new List<Product>();
        private List<ProductBrand> fakerProductBrandList = new List<ProductBrand>();
        private List<ProductType> fakerProductTypeList = new List<ProductType>();
        public AcessoIngressoInitializer(AcessoIngressoContext context)
        {
            _context = context;
          
        }

        private void FactoryProductBrands()
        {

            if (!_context.ProductBrands.Any())
            {
                var productBrand = new Faker<ProductBrand>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .Generate(100);

                fakerProductBrandList.AddRange(productBrand);

                _context.ProductBrands.AddRange(fakerProductBrandList);
                _context.SaveChanges();
            }
        }

        private void FactoryProductTypes()
        {

            if (!_context.ProductTypes.Any())
            {
                var productType = new Faker<ProductType>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .Generate(100);

                fakerProductTypeList.AddRange(productType);

                _context.ProductTypes.AddRange(fakerProductTypeList);
                _context.SaveChanges();
            }
        }

        private void FactoryProducts()
        {
            
            if (!_context.Products.Any()){
                var category = new Faker<Product>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .RuleFor(p => p.Description, p => p.Commerce.Product())
                    .RuleFor(p => p.Price, p => p.Random.Decimal(10, 100))
                    .RuleFor(p => p.PictureUrl, p => p.Internet.Avatar())
                    .Generate(100);

                fakerProductList.AddRange(category);
                
                _context.Products.AddRange(fakerProductList);
                _context.SaveChanges();
            }
    }

        public void SeedDemo()
        {
            FactoryProductBrands();
            // FactoryProductTypes();
            // FactoryProducts();
        }
    }
}