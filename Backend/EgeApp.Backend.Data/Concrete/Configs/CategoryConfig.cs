using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EgeApp.Backend.Models;
using System.Collections.Generic;

namespace EgeApp.Backend.Data.Concrete.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.IsHome)
                .IsRequired()
                .HasDefaultValue(false); 

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("date('now')");

            builder.Property(x => x.ModifiedDate)
                .HasDefaultValueSql("date('now')");

            
            List<Category> categories = new()
            {
                new() { Id = 1, Name = "Ortopedik Ürünler", Description = "Ortopedik ürünler", IsActive = true, IsHome = true, Url = "ortopedik-urunler", ImageUrl = null },
                new() { Id = 2, Name = "Solunum Cihazları", Description = "Solunum cihazları", IsActive = true, IsHome = true, Url = "solunum-cihazlari", ImageUrl = null },
                new() { Id = 3, Name = "Solunum Maskeleri", Description = "Solunum maskeleri", IsActive = true, IsHome = false, Url = "solunum-maskeleri", ImageUrl = null },
                new() { Id = 4, Name = "Hasta Bakım Ürünleri", Description = "Hasta bakım ürünleri", IsActive = true, IsHome = false, Url = "hasta-bakim-urunleri", ImageUrl = null },
                new() { Id = 5, Name = "Tıbbi Test ve Sarf Malzemeleri", Description = "Tıbbi test ve sarf malzemeleri", IsActive = true, IsHome = true, Url = "tibbi-test-ve-sarf-malzemeleri", ImageUrl = null },
                new() { Id = 6, Name = "Tansiyon ve Nabız Ölçüm Cihazları", Description = "Tansiyon ve nabız ölçüm cihazları", IsActive = true, IsHome = false, Url = "tansiyon-ve-nabiz-olcum-cihazlari", ImageUrl = null }
            };

            builder.HasData(categories);
            builder.ToTable("Categories");
        }
    }
}