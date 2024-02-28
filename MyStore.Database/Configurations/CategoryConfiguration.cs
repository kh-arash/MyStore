using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Database.Models.Category;

namespace MyStore.Database.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.Property(e => e.Description).IsUnicode(true);
            builder.Property(e => e.Description).HasMaxLength(200);

            builder.Property(e => e.Title).IsUnicode(true);
            builder.Property(e => e.Title).HasMaxLength(100);

            builder.ToTable("Category");

        }
    }
}
