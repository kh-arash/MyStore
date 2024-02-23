using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyStore.Database.Models.Category;

namespace MyStore.Database.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategoryModel>
    {
        public void Configure(EntityTypeBuilder<SubCategoryModel> builder)
        {
            builder.Property(e => e.Description).IsUnicode(true);
            builder.Property(e => e.Description).HasMaxLength(200);

            builder.Property(e => e.Title).IsUnicode(true);
            builder.Property(e => e.Title).HasMaxLength(100);

        }
    }
}
