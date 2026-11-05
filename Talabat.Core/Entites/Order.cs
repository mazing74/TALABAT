using Talabat.Core.Entites.OrderAggregation;

namespace Talabat.Core.Entites
{
    public class Order : BaseEntity
    {
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }
        public Order()
        {
                
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.pending; //InitValue
        public Address ShippingAddress { get; set; }
        // this not a nav prop because address is not table in db is called Aggregation Property
        // انا عايزو ياخد الحاجه الي في ال ادريس ويحطها هنا كا فيلدز عاديه خالص زي ما عملنا في ال اوردر ايتيم
        // طب ليه معملتهاش كدا ؟ عشان لما يجي يعملها مابينج يسيميها
        // ShippingAddress.Country , ShippingAddress.FirstName وهكذا 


         public DeliveryMethod DeliveryMethod { get; set; } //O2M
        // this is nav prop because  DeliveryMethod Is Table In Db
        // بس بص طالما مش حاطط كولكشن للاوردر في ال دليفري ميثود انتيتي فريم ورك هيفهم انها وان تو وان
        // طب انا ليه محطتهاش ؟ عشان مش عايز انا حر  
        // و انتيتي فريم ورك هيروح يزو كولمن لل فورين كيي هنا كدا كدا علي اساس انها وان تو وان 

       public  ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); //o2m
        // this is nav prop because  OrderItem Is Table In Db
        public decimal SubTotal { get; set; } //Price*Quantity
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        // عملتو فانكشن عشان مش محتاج اخزنو في الداتا بيز
        public string PaymentIntentId { get; set; } = string.Empty; 

    }
}
