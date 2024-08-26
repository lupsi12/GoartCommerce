using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Domain.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId);

            builder.HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic items" },
                new Category { Id = 2, Name = "Clothing", Description = "Clothing and accessories" },
                new Category { Id = 3, Name = "Books", Description = "Books of all genres" },
                new Category { Id = 4, Name = "Home & Kitchen", Description = "Home and kitchen appliances" },
                new Category { Id = 5, Name = "Toys & Games", Description = "Toys and games for children" },
                new Category { Id = 6, Name = "Sports & Outdoors", Description = "Sports equipment and outdoor gear" },
                new Category { Id = 7, Name = "Beauty & Health", Description = "Beauty products and health supplies" },
                new Category { Id = 8, Name = "Automotive", Description = "Automotive parts and accessories" },
                new Category { Id = 9, Name = "Jewelry", Description = "Jewelry and watches" },
                new Category { Id = 10, Name = "Music", Description = "Musical instruments and accessories" }
            );
        }
    }
}
