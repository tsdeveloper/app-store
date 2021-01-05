using System;
using System.Linq;
using Infrastructure.Data;
using Core.Entities;
using Bogus;
using System.Collections.Generic;

namespace Infrastructure.SeedData
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

            if (!_context.DbSet<ProductBrand>().Any())
            {
                var productBrand = new Faker<ProductBrand>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .Generate(100);

                fakerProductBrandList.AddRange(productBrand);

                _context.DbSet<ProductBrand>().AddRange(fakerProductBrandList);
                _context.SaveChanges();
            }
        }

        private void FactoryProductTypes()
        {

            if (!_context.DbSet<ProductType>().Any())
            {
                var productType = new Faker<ProductType>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .Generate(100);

                fakerProductTypeList.AddRange(productType);

                _context.DbSet<ProductType>().AddRange(fakerProductTypeList);
                _context.SaveChanges();
            }
        }

        private void FactoryProducts()
        {
            
            if (!_context.DbSet<Product>().Any()){
                var category = new Faker<Product>()
                    .RuleFor(p => p.Id, p => p.Random.Guid())
                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                    .RuleFor(p => p.Description, p => p.Commerce.Product())
                    .RuleFor(p => p.Price, p => p.Random.Decimal(10, 100))
                    .RuleFor(p => p.PictureUrl, p => p.Internet.Avatar())
                    .Generate(100);

                fakerProductList.AddRange(category);
                
                _context.DbSet<Product>().AddRange(fakerProductList);
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