using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositries;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasketAsync(string Basket_Id)
        {
            var basket = await _database.StringGetAsync(Basket_Id);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }


        public async Task<bool> DeleteBasket(string Basket_Id)
        {
            return await _database.KeyDeleteAsync(Basket_Id);
        }


        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (CreatedOrUpdatedBasket == false)
                return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
