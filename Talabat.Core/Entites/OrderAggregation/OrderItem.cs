namespace Talabat.Core.Entites.OrderAggregation
{
    public class OrderItem :BaseEntity
    {
        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
                
        }

        public int OrderId { get; set; } 
        public ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
 