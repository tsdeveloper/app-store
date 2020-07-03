using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Factory
{
    public class BuildFactoryFake
    {
        private static List<Product> fakerProductList = new List<Product>();
        private static List<ProductBrand> fakerProductBrandList = new List<ProductBrand>();
        private static List<ProductType> fakerProductTypeList = new List<ProductType>();
        private static List<Client> fakerClientList = new List<Client>();
        private static List<Event> fakerEventList = new List<Event>();
        private static List<Ticket> fakerTickerList = new List<Ticket>();
        private static List<string> listImagesProduct = new List<string>();
        private static List<SortOption> fakerSortOptionList;

        private const string URLAPI = @"localhost:5001/api";
        private const string FILE_JSON_PRODUCT_BRANDS = @"../Infrastructure/SeedData/ProductBrands.json";
        private const string FILE_JSON_PRODUCT_TYPES = @"../Infrastructure/SeedData/ProductTypes.json";
        private const string FILE_JSON_PRODUCT = @"../Infrastructure/SeedData/Product.json";
        private const string FILE_JSON_EVENT = @"../Infrastructure/SeedData/Event.json";
        private const string FILE_JSON_CLIENT = @"../Infrastructure/SeedData/Client.json";
        private const string FILE_JSON_TICKET = @"../Infrastructure/SeedData/Ticket.json";
        private const string FILE_JSON_SORT_OPTION = @"../Infrastructure/SeedData/SortOption.json";
        private const string FILE_IMAGES = @"../Infrastructure/SeedData/Images";

        public static async Task BuildFactoryAsync(AppStoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await GenerateBuildFactoryClient(context,
                    loggerFactory,
                    true);

                await GenerateBuildFactoryEvent(context,
                    loggerFactory,
                    true);

                await GenerateBuildFactoryTicker(context,
                    loggerFactory,
                    true);

                await GenerateBuildFactoryProductBrand(context,
                    loggerFactory,
                    true);
                await GenerateBuildFactoryProductType(context,
                    loggerFactory,
                    true);
                await GenerateBuildFactoryProduct(context,
                    loggerFactory,
                    true);

                await GenerateBuildFactorySortOption(context,
                    loggerFactory,
                    true);
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                logger.LogError(ex.Message);
            }
        }

        private static async Task GenerateBuildFactoryClient(AppStoreContext context, ILoggerFactory loggerFactory,
            bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<Client>().Any())

                            if (File.Exists(FILE_JSON_CLIENT))
                            {
                                File.Delete(FILE_JSON_CLIENT);
                            }

                        var clientIds= 1;
                        var clientFaker = new Faker<Client>()
                            .RuleFor(p => p.Id, p => clientIds++)
                            .RuleFor(p => p.Name, p => p.Person.FirstName)
                            .RuleFor(p => p.CpfCnpj, p => p.Person.Cpf(false))
                            .RuleFor(p => p.Address, p => p.Person.Address.Street)
                            .RuleFor(p => p.Phone1, p => p.Person.Phone)
                            .RuleFor(p => p.Phone2, p => p.Person.Phone)
                            .Generate(100);

                        fakerClientList.AddRange(clientFaker);
                        using (var file = File.CreateText(FILE_JSON_CLIENT))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerClientList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }

        private static async Task GenerateBuildFactoryEvent(AppStoreContext context, ILoggerFactory loggerFactory,
            bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<Event>().Any())

                            if (File.Exists(FILE_JSON_EVENT))
                            {
                                File.Delete(FILE_JSON_EVENT);
                            }

                        var eventIds= 1;
                        var eventFaker = new Faker<Event>()
                            .RuleFor(p => p.Id, p => eventIds++)
                            .RuleFor(p => p.CodePublish, p => p.Random.Guid().ToString().Substring(0, 8))
                            .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                            .RuleFor(p => p.Description, p => p.Commerce.ProductName())
                            .Generate(100);

                        fakerEventList.AddRange(eventFaker);

                        foreach (var eventClient in fakerEventList)
                        {
                            foreach (var client in fakerClientList)
                            {
                                var clientExist = fakerEventList.Any(x => x.ClientId.Equals(client.Id));
                                if (!clientExist)
                                {
                                    eventClient.ClientId = client.Id;
                                    continue;
                                }
                            }

                            eventClient.PublishUrl =
                                $@"{URLAPI}/eventos/evento-cliente/{eventClient.CodePublish}/ingressos/";
                        }

                        using (var file = File.CreateText(FILE_JSON_EVENT))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerEventList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }

        private static async Task GenerateBuildFactoryTicker(AppStoreContext context, ILoggerFactory loggerFactory,
            bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<Ticket>().Any())

                            if (File.Exists(FILE_JSON_TICKET))
                            {
                                File.Delete(FILE_JSON_TICKET);
                            }

                        var tickerIds= 1;
                        var tickerFaker = new Faker<Ticket>()
                            .RuleFor(p => p.Id, p => tickerIds++)
                            .RuleFor(p => p.Description, p => p.Commerce.ProductName())
                            .RuleFor(p => p.NameDisplayUrl, p => p.Commerce.ProductName())
                            .RuleFor(p => p.Quantity, p => p.Random.Number(1, 10))
                            .RuleFor(p => p.Price, p => p.Random.Decimal(10, 20))
                            .Generate(4);

                        fakerTickerList.AddRange(tickerFaker);

                        foreach (var ticker in fakerTickerList)
                        {
                            foreach (var eventClient in fakerEventList)
                            {
                                var eventExist = fakerTickerList.Any(x => x.EventId.Equals(eventClient.Id));
                                if (!eventExist)
                                {
                                    ticker.EventId = eventClient.Id;
                                    break;
                                }
                            }
                        }

                        using (var file = File.CreateText(FILE_JSON_TICKET))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerTickerList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }


        private static async Task GenerateBuildFactoryProductBrand(AppStoreContext context,
            ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<ProductBrand>().Any())

                            if (File.Exists(FILE_JSON_PRODUCT_BRANDS))
                            {
                                File.Delete(FILE_JSON_PRODUCT_BRANDS);
                            }

                        var productBrandIds= 1;
                        var productBrand = new Faker<ProductBrand>()
                            .RuleFor(p => p.Id, p => productBrandIds++)
                            .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                            .Generate(100);

                        fakerProductBrandList.AddRange(productBrand);
                        using (var file = File.CreateText(FILE_JSON_PRODUCT_BRANDS))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerProductBrandList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }

        private static async Task GenerateBuildFactoryProductType(AppStoreContext context,
            ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<ProductType>().Any())

                            if (File.Exists(FILE_JSON_PRODUCT_TYPES))
                            {
                                File.Delete(FILE_JSON_PRODUCT_TYPES);
                            }

                        var productTypeIds= 1;
                        var productType = new Faker<ProductType>()
                            .RuleFor(p => p.Id, p => productTypeIds++)
                            .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                            .Generate(100);

                        fakerProductTypeList.AddRange(productType);


                        using (var file = File.CreateText(FILE_JSON_PRODUCT_TYPES))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerProductTypeList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }

        private static async Task GenerateBuildFactoryProduct(AppStoreContext context,
            ILoggerFactory loggerFactory, bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!context.DbSet<Product>().Any())

                        if (File.Exists(FILE_JSON_PRODUCT))
                        {
                            File.Delete(FILE_JSON_PRODUCT);
                        }


                    if (Directory.Exists(FILE_IMAGES))
                    {
                        var ext = new List<string> {".jpg", ".png"};

                        var imageFile = Directory.GetFiles(FILE_IMAGES, "*.*", SearchOption.AllDirectories)
                            .Where(c => ext.Contains(Path.GetExtension(c).ToLowerInvariant()))
                            .Select(c => new
                            {
                                fileName = new FileInfo(c).Name
                            }).ToList();

                        imageFile.ForEach(x => listImagesProduct.Add(x.fileName));
                    }

                    var productIds= 1;
                    var productList = new Faker<Product>()
                        .RuleFor(p => p.Id, p => productIds++)
                        .RuleFor(p => p.Name, p => p.Commerce.ProductName())
                        .RuleFor(p => p.Description, p => p.Commerce.Product())
                        .RuleFor(p => p.Price, p => p.Random.Decimal(10, 100))
                        .RuleFor(p => p.PictureUrl,
                            p => p.PickRandom(listImagesProduct) ?? "https://dummyimage.com/300")
                        .Generate(100);

                    foreach (var product in productList)
                    {
                        foreach (var productBrand in fakerProductBrandList)
                        {
                            var productExist = productList.Any(x => x.ProductBrandId.Equals(productBrand.Id));
                            if (!productExist)
                            {
                                product.ProductBrandId = productBrand.Id;
                                continue;
                            }
                        }

                        foreach (var productType in fakerProductTypeList)
                        {
                            var productExist = productList.Any(x => x.ProductTypeId.Equals(productType.Id));
                            if (!productExist)
                            {
                                product.ProductTypeId = productType.Id;
                                continue;
                            }
                        }
                    }

                    fakerProductList.AddRange(productList);
                    using (var file = File.CreateText(FILE_JSON_PRODUCT))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, fakerProductList);
                    }
                }
                catch (System.Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                    logger.LogError(ex.Message);
                }
            }).Wait();
        }


        private static async Task GenerateBuildFactorySortOption(AppStoreContext context, ILoggerFactory loggerFactory,
            bool runBuildFactory = false)
        {
            Task.Run(() =>
            {
                if (runBuildFactory)
                {
                    try
                    {
                        if (!context.DbSet<SortOption>().Any())

                            if (File.Exists(FILE_JSON_SORT_OPTION))
                            {
                                File.Delete(FILE_JSON_SORT_OPTION);
                            }

                        var indexSortOption = 1;
                        fakerSortOptionList = new List<SortOption>
                        {
                            new SortOption
                            {
                                Id = indexSortOption++, Name = "Alphabetical", Description = "Order by por name",
                                Value = "name"
                            },
                            new SortOption
                            {
                                Id = indexSortOption++, Name = "Price: Low to High", Description = "Price: Low to High",
                                Value = "priceAsc"
                            },
                            new SortOption
                            {
                                Id = indexSortOption, Name = "Price: High to Low", Description = "Price: Low to High",
                                Value = "priceDesc"
                            },
                        };


                        using (var file = File.CreateText(FILE_JSON_SORT_OPTION))
                        {
                            var serializer = new JsonSerializer();
                            serializer.Serialize(file, fakerSortOptionList);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                        logger.LogError(ex.Message);
                    }
                }
            }).Wait();
        }
    }
}