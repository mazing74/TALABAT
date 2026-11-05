using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.OrderAggregation
{
     public enum OrderStatus
    {
        [EnumMember(Value = "Pendind")]
        pending ,
        [EnumMember(Value ="Payment_Recived")]
        PaymentRecived ,
        [EnumMember(Value = "Payment_Failed")]
        PaymenFailed,


    }
}
