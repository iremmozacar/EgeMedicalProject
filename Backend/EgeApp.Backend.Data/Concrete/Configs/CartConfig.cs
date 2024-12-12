using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EgeApp.Backend.Entity.Concrete;

namespace EgeApp.Backend.Data.Concrete.Configs
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasData(
                new Cart { Id = 1, CreatedDate = DateTime.Now, UserId = "1" },
                new Cart { Id = 2, CreatedDate = DateTime.Now, UserId = "2" },
                new Cart { Id = 3, CreatedDate = DateTime.Now, UserId = "3" },
                new Cart { Id = 4, CreatedDate = DateTime.Now, UserId = "4" },
                new Cart { Id = 5, CreatedDate = DateTime.Now, UserId = "5" },
                new Cart { Id = 6, CreatedDate = DateTime.Now, UserId = "6" }
            );
        }
    }
}
