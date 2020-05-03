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
        private static List<Client> clients;
        private static List<Event> events;
        private static List<Ticket> tickets;
        private static List<ProductBrand> brands;
        private static List<ProductType> productTypes;

        private const string FILE_JSON_PRODUCT_BRANDS = @"../Infrastructure/SeedData/ProductBrands.json";
        private const string FILE_JSON_PRODUCT_TYPES = @"../Infrastructure/SeedData/ProductTypes.json";
        private const string FILE_JSON_PRODUCT = @"../Infrastructure/SeedData/Product.json";
        private const string FILE_JSON_EVENT = @"../Infrastructure/SeedData/Event.json";
        private const string FILE_JSON_CLIENT = @"../Infrastructure/SeedData/Client.json";
        private const string FILE_JSON__TICKET= @"../Infrastructure/SeedData/Ticket.json";

        public static async Task SeedAsync(AcessoIngressoContext context, ILoggerFactory loggerFactory)
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

        private static async Task GenerateBuildFactoryClient(AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false)
        {
            if (runSeed && !context.DbSet<Client>().Any())
            {
                try
                {
                    var clientsData = File.ReadAllText(FILE_JSON_CLIENT);

                    clients = JsonConvert.DeserializeObject<List<Client>>(clientsData);

                    context.DbSet<Client>().AddRange(clients);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed Client db");
                }
            }


            clients = context.DbSet<Client>().ToList();
        }

        private static async Task GenerateBuildFactoryEvent(AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false)
        {
            if (runSeed && !context.DbSet<Event>().Any())
            {
                try
                {
                    var EventsData = File.ReadAllText(FILE_JSON_EVENT);

                    events = JsonConvert.DeserializeObject<List<Event>>(EventsData);

                    context.DbSet<Event>().AddRange(events);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed Event db");
                }
            }


            events = context.DbSet<Event>().ToList();
        }

        private static async Task GenerateBuildFactoryTicker(AcessoIngressoContext context, ILoggerFactory loggerFactory, bool runSeed = false)
        {
            if (runSeed && !context.DbSet<Ticket>().Any())
            {
                try
                {
                    var tickersData = File.ReadAllText(FILE_JSON__TICKET);

                    tickets = JsonConvert.DeserializeObject<List<Ticket>>(tickersData);

                    context.DbSet<Ticket>().AddRange(tickets);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed Ticker db");
                }
            }


            tickets = context.DbSet<Ticket>().ToList();
        }

        private static async Task GenerateSeedProductBrand(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.DbSet<ProductBrand>().Any())
            {
                try
                {
                    var brandsData = File.ReadAllText(FILE_JSON_PRODUCT_BRANDS);

                    brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                    context.DbSet<ProductBrand>().AddRange(brands);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed ProductBrand db");
                }
            }


            brands = context.DbSet<ProductBrand>().ToList();
        }

        private static async Task GenerateSeedProductType(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.DbSet<ProductType>().Any())
            {
                try
                {
                    var productTypesData = File.ReadAllText(FILE_JSON_PRODUCT_TYPES);

                    productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypesData);

                    context.DbSet<ProductType>().AddRange(productTypes);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<AcessoIngressoContextSeed>();
                    logger.LogError(ex, "An error occured during seed ProductType db");
                }
            }

            productTypes = context.DbSet<ProductType>().ToList();
        }

        private static async Task GenerateSeedProduct(AcessoIngressoContext context, ILoggerFactory loggerFactory,
            bool runSeed = false)
        {
            if (runSeed && !context.DbSet<Product>().Any())
            {
                try
                {
                    var productsData = File.ReadAllText(FILE_JSON_PRODUCT);

                    var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                    context.DbSet<Product>().AddRange(products);
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