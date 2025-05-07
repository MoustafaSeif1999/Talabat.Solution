using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configrations
{
    internal class Product_Config : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(P=> P.Name).IsRequired().HasMaxLength(100);

            builder.Property(P=> P.Description).IsRequired();

            builder.Property(P=> P.PictureUrl).IsRequired();

            builder.HasOne(P => P.ProductBrand).WithMany()
                   .HasForeignKey(P => P.ProductBrandId);

            builder.HasOne(P=> P.ProductType).WithMany()
                   .HasForeignKey(P=> P.ProductTypeId);


        }
    }
}
