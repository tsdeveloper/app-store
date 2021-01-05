using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> b)
        {
            b.Property(p => p.Id).IsRequired();
            
            b.Property(p => p.Name).IsRequired().HasMaxLength(100);
            b.Property(p => p.Description).IsRequired();
            b.Property(p => p.Price).HasColumnType("decimal(18,2)");
            b.Property(p => p.PictureUrl).IsRequired();
            b.Property(p => p.IsCanceled).HasDefaultValue(false);
            b.HasOne(p => p.ProductBrand).WithMany()
                    .HasForeignKey(br => br.ProductBrandId);
            b.HasOne(p => p.ProductType).WithMany()
                    .HasForeignKey(t => t.ProductTypeId);
        }
    }
}