using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class EventConfig: IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> b)
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name).HasMaxLength(200).IsRequired(true);
            b.Property(e => e.Description).HasMaxLength(200).IsRequired(false);
            b.Property(e => e.PublishUrl).HasMaxLength(250).IsRequired(false);
            b.Property(e => e.IsCanceled).HasDefaultValue(false);
            
            b.HasOne(p => p.Client).WithMany()
                                                                  .HasForeignKey(br => br.ClientId);
        }
    }
}