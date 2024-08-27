﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired(); 

            builder.Ignore(c => c.TotalPrice); 

            builder.HasMany(c => c.CartDetails)
                .WithOne(cd => cd.Cart)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}