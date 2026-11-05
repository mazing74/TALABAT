using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregation;

namespace Talabat.Repository
{
    public class TalabatContext :DbContext 
    {
        public TalabatContext(DbContextOptions<TalabatContext> options) :base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalabatContext).Assembly);
            base.OnModelCreating(modelBuilder); 

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }



    }
}
