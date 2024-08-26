using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id); 

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.Description)
                .HasMaxLength(500); 

            builder.Property(p => p.Stock)
                .IsRequired(); 

            builder.Property(p => p.Price)
                .IsRequired();

            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId);


            builder.Property(p => p.UserId) 
      .IsRequired();
        }
    }
}
