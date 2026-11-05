using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregation;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)
                .HasConversion(OS => OS.ToString(), OS => (OrderStatus) Enum.Parse(typeof(OrderStatus), OS));
            // ثاني قوس لما ترجع هترجع اي اول واحد لما اخزنها هخزنه اي ؟

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");// عشان اشيل الورنينج بتاع الكونسول بس

            // لما تيجي تعمل مابينج للادريس حط البروبيرزتز بتاعتو مع ال اونر بتاعو اكنهم واحد
            builder.OwnsOne(o => o.ShippingAddress, SA => SA.WithOwner());

            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);

        }
    }
}
