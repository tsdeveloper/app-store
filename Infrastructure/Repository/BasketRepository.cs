﻿using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(data);
            
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.CustomerBasketId, JsonConvert.SerializeObject(basket), TimeSpan.FromDays(30));

            if (!created) return null;


            return await GetBasketAsync(basket.CustomerBasketId);

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }
    }
}