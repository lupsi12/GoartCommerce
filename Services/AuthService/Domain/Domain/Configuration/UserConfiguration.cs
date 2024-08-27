using Domain.Entities;
using Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasData(
            new User
            {
                Id = 1,
                Name = "admin",
                LastName = "",
                Email = "admin@gmail.com",
                Password = "1234",
                BirthDate = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Role = Roles.ADMIN, 
                Status = Status.APPROVED, 
                CreatedDate = DateTime.Now,
                IsDeleted = false
            }
        );
        }
    }
}
