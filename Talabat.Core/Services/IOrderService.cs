using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregation;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Entites.Order> CreateOrderAsync(string BuyerEmail,string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Entites.Order>> GetOrdersForSpecificUser(string BuyerEmail);
        Task<Entites.Order> GetOrderByIdForSpecificUser(string BuyerEmail, int OrderId);
    }
}
