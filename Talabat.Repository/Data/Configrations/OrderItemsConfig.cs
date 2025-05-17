using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderItemsConfig : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(orderitem => orderitem.Product , NP => NP.WithOwner());

            builder.Property(orderItems => orderItems.Price).HasColumnType("decimal(18,2)");
        }
    }
}
