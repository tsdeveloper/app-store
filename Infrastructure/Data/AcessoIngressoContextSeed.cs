using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class AcessoIngressoContextSeed
    {
        private static List<ProductBrand> brands;
        private static List<ProductType> productTypes;

        private const string FILE_JSON_PRODUCT_BRANDS = @"../Infrastructure/SeedData/ProductBrands.json";
        private const string FILE_JSON_PRODUCT_TYPES = @"../Infrastructure/SeedData/ProductTypes.json";
        private const string FILE_JSON_PRODUCT = @"../Infrastructure/SeedData/Product.json";

        public static async Task SeedAsync(AcessoIngressoContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await GenerateSeedProductBrand(context,
                    loggerFactory,
                    true);
                await GenerateSeedProductType(context,
                    loggerFactory,
                    true);
                await GenerateSeedProduct(context,
                    loggerFactory,
                    true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static async Task GenerateSeedProductBrand(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.ProductBrands.Any())
            {
                try
                {
                    var brandsData = File.ReadAllText(FILE_JSON_PRODUCT_BRANDS);

                    brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    context.ProductBrands.AddRange(brands);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed ProductBrand db");
                }
            }


            brands = context.ProductBrands.ToList();
        }

        private static async Task GenerateSeedProductType(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.ProductTypes.Any())
            {
                try
                {
                    var productTypesData = File.ReadAllText(FILE_JSON_PRODUCT_TYPES);

                    productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypesData);

                    context.ProductTypes.AddRange(productTypes);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed ProductType db");
                }
            }

            productTypes = context.ProductTypes.ToList();
        }

        private static async Task GenerateSeedProduct(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.Products.Any())
            {
                try
                {
                    var productsData = File.ReadAllText(FILE_JSON_PRODUCT);

                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);


                    foreach (var product in products)
                    {
                        foreach (var brand in brands)
                        {
                            var resultExist = context.Products.Any(x => x.ProductBrandId.Equals(brand.Id));
                            if (!resultExist)
                            {
                                product.ProductBrand = brand;
                                break;
                            }
                        }

                        foreach (var productType in productTypes)
                        {
                            var resultExist = context.Products.Any(x => x.ProductTypeId.Equals(productType.Id));
                            if (!resultExist)
                            {
                                product.ProductType = productType;
                                break;
                            }
                        }
                    }

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed Product db");
                    ;
                }
            }
        }
    }
}