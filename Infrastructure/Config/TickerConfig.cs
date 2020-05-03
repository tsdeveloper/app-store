using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class TickerConfig: IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> b)
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Description).HasMaxLength(200).IsRequired(true);
            b.Property(e => e.Price).HasMaxLength(200).IsRequired(true);
            b.Property(e => e.Quantity).HasMaxLength(250).IsRequired(true);
            b.Property(e => e.NameDisplayUrl).HasMaxLength(200).IsRequired(true);
            b.Property(e => e.IsCanceled).HasDefaultValue(false);
            
            b.HasOne(p => p.Event).WithMany()
                .HasForeignKey(br => br.EventId);
        }
        
    }
}