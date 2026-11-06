using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregation;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            var Basket = await _basketRepository.GetBasketAsync(BasketId);



            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder, Product.Price, item.Quantity);
                    OrderItems.Add(OrderItem);

                }
            }

            var SupTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SupTotal);

            await _unitOfWork.Repository<Order>().AddAsync(Order);

            int Result = await _unitOfWork.CompeleteAsync();

            if (Result <= 0) return null;
           
            return Order;

        }

        public Task<Order> GetOrderByIdForSpecificUser(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
