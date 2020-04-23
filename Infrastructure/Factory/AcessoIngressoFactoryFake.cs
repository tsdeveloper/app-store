using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Factory {
    public class AcessoIngressoFactoryFake 
    {

        private static List<Product> fakerProductList = new List<Product>();
        private static List<ProductBrand> fakerProductBrandList = new List<ProductBrand>();
        private static List<ProductType> fakerProductTypeList = new List<ProductType>();

        private const string FILE_JSON_PRODUCT_BRANDS = @"../SeedData/ProductBrands.json";
        private const string FILE_JSON_PRODUCT_TYPES = @"../SeedData/ProductTypes.json";
        private const string FILE_JSON_PRODUCT = @"../SeedData/Product.json";

        public static async Task BuildFactoryAsync (AcessoIngressoContext context, ILoggerFactory loggerFactory) {
            try {
                await GenerateBuildFactoryProductBrand (context,
                    loggerFactory,
                    true);
                await GenerateBuildFactoryProductType (context,
                    loggerFactory,
                    true);
                await GenerateBuildFactoryProduct (context,
                    loggerFactory,
                    true);
            }  catch (System.Exception ex) {

                var logger = loggerFactory.CreateLogger<AcessoIngressoFactoryFake>();
                logger.LogError(ex.Message);
            }

        }

        private static async Task GenerateBuildFactoryProductBrand(AcessoIngressoContext context,
            ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {
           Task.Run(() => 
            {
                if (runBuildFactory) {
                
                    try {
                        if (!context.ProductBrands.Any ())

                            if (!File.Exists (FILE_JSON_PRODUCT_BRANDS)) {
                                if (File.ReadAllText (FILE_JSON_PRODUCT_BRANDS) == null)
                                    File.Delete (FILE_JSON_PRODUCT_BRANDS);

                                var productBrand = new Faker<ProductBrand>()
                                    .RuleFor(p => p.Id, p => p.Random.Guid())
                                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                                    .Generate(100);

                                fakerProductBrandList.AddRange(productBrand);
                                using (var file = File.CreateText(FILE_JSON_PRODUCT_BRANDS))
                                {
                                    var serializer = new JsonSerializer();
                                    serializer.Serialize(file, fakerProductBrandList);
                                }

                            }

                    }  catch (System.Exception ex) {

                        var logger = loggerFactory.CreateLogger<AcessoIngressoFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        

        }

        private static async Task GenerateBuildFactoryProductType (AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {

            Task.Run(() =>
            {
                if (runBuildFactory)
                {

                    try
                    {
                        if (!context.ProductTypes.Any())

                            if (!File.Exists(FILE_JSON_PRODUCT_TYPES))
                            {
                                if (File.ReadAllText(FILE_JSON_PRODUCT_TYPES) == null)
                                    File.Delete(FILE_JSON_PRODUCT_TYPES);

                                var productType = new Faker<ProductType>()
                                    .RuleFor(p => p.Id, p => p.Random.Guid())
                                    .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                                    .Generate(100);

                                fakerProductTypeList.AddRange(productType);
                                using (var file = File.CreateText(FILE_JSON_PRODUCT_TYPES))
                                {
                                    var serializer = new JsonSerializer();
                                    serializer.Serialize(file, fakerProductTypeList);
                                }

                            }

                    }
                    catch (System.Exception ex)
                    {

                        var logger = loggerFactory.CreateLogger<AcessoIngressoFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();

        }

        private static async Task GenerateBuildFactoryProduct(AcessoIngressoContext context,
            ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!context.Products.Any())

                        if (!File.Exists(FILE_JSON_PRODUCT))
                        {
                            if (File.ReadAllText(FILE_JSON_PRODUCT) == null)
                                File.Delete(FILE_JSON_PRODUCT);

                            var product = new Faker<Product>()
                                .RuleFor(p => p.Id, p => p.Random.Guid())
                                .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                                .RuleFor(p => p.Description, p => p.Commerce.Product())
                                .RuleFor(p => p.Price, p => p.Random.Decimal(10, 100))
                                .RuleFor(p => p.PictureUrl, p => p.Internet.Avatar())
                                .Generate(100);

                            fakerProductList.AddRange(product);
                            using (var file = File.CreateText(FILE_JSON_PRODUCT))
                            {
                                var serializer = new JsonSerializer();
                                serializer.Serialize(file, fakerProductList);
                            }

                        }

                }
                catch (System.Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<AcessoIngressoFactoryFake>();
                    logger.LogError(ex.Message);
                }
            }).Wait();
        }
    }
}