using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(cd => cd.Id);

            builder.Property(cd => cd.Quantity)
                .IsRequired();

            builder.Property(cd => cd.PricePerUnit)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(cd => cd.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(cd => cd.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(cd => cd.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cd => cd.ProductId);
        }
    }
}
