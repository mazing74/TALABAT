using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Nodes;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _database.StringGetAsync(basketId);

            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UbdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);

            var CreatedOrUbdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromDays(7));

            if (!CreatedOrUbdated) return null;

            return await GetBasketAsync(basket.Id);

        }
    }
}
