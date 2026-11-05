using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.OrderAggregation
{
    public class DeliveryMethod :BaseEntity
    {
        public DeliveryMethod(string shortName, string describtion, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = describtion;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }
        public DeliveryMethod()
        {
                
        }
        public string ShortName { get; set; }
        public string Description { get; set; } 
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
