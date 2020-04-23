using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data {
    public class AcessoIngressoContextSeed {

         private const string FILE_JSON_PRODUCT_BRANDS = @"../SeedData/ProductBrands.json";
        private const string FILE_JSON_PRODUCT_TYPES = @"../SeedData/ProductTypes.json";
        private const string FILE_JSON_PRODUCT = @"../SeedData/Product.json";

        public static async Task SeedAsync (AcessoIngressoContext context, ILoggerFactory loggerFactory) {
            try {
                await GenerateSeedProductBrand (context,
                    loggerFactory,
                    true);
                await GenerateSeedProductType (context,
                    loggerFactory,
                    true);
                await GenerateSeedProduct (context,
                    loggerFactory,
                    true);
            } catch (System.Exception) {

                throw;
            }

        }

        private static async Task GenerateSeedProductBrand (AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false) {
            if (runSeed) {
                try {
                    var brandsData = File.ReadAllText (FILE_JSON_PRODUCT_BRANDS);

                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>> (brandsData);

                    context.ProductBrands.AddRange (brands);

                    await context.SaveChangesAsync ();

                } catch (System.Exception) {

                    throw;
                }
            }

        }

        private static async Task GenerateSeedProductType (AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false) {
            if (runSeed) {
                try {
                    var productTypesData = File.ReadAllText (FILE_JSON_PRODUCT_TYPES);

                    var productTypes = JsonConvert.DeserializeObject<List<ProductType>> (productTypesData);

                    context.ProductTypes.AddRange (productTypes);

                    await context.SaveChangesAsync ();

                } catch (System.Exception) {

                    throw;
                }
            }

        }

        private static async Task GenerateSeedProduct (AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false) {
            if (runSeed) {
                try {
                    var brandsData = File.ReadAllText (FILE_JSON_PRODUCT);

                    var brands = JsonConvert.DeserializeObject<List<ProductBrand>> (brandsData);

                    context.ProductBrands.AddRange (brands);

                    await context.SaveChangesAsync ();

                } catch (System.Exception) {

                    throw;
                }
            }

        }
    }
}