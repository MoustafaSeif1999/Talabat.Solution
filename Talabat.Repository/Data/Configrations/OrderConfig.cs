using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Orders_Aggrigate;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(
                        Ostatus => Ostatus.ToString(),
                        Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus)

                );
            
            builder.HasMany( O => O.Items ).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
