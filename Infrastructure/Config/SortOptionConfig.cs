using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class SortOptionConfig : IEntityTypeConfiguration<SortOption>
    {
        public void Configure(EntityTypeBuilder<SortOption> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(30).IsRequired(true);
            b.Property(c => c.Description).HasMaxLength(200).IsRequired(false);
            b.Property(c => c.Value).HasMaxLength(100).IsRequired(true);
            b.Property(c => c.IsCanceled).HasDefaultValue(false);
        }
    }
}