using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Database.Models.Product;

namespace MyStore.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.Property(e => e.Model).HasMaxLength(60);

            builder.Property(e => e.Price).HasColumnType("decimal(10,2)");

            builder.Property(e => e.Description).IsUnicode(true);
            builder.Property(e => e.Description).HasColumnType("ntext");

            builder.HasMany(e => e.Categories).WithMany(e => e.Products).UsingEntity(join => join.ToTable("ProductCategories"));

            builder.HasMany(e => e.ProductImages).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);

            builder.ToTable("Product");
        }

    }
}
