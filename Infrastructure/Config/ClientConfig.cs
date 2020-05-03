using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(200).IsRequired(true);
            b.Property(c => c.CpfCnpj).HasMaxLength(200).IsRequired(false);
            b.Property(c => c.Address).HasMaxLength(200).IsRequired(false);
            b.Property(c => c.Phone1).HasMaxLength(200).IsRequired(false);
            b.Property(c => c.Phone2).HasMaxLength(200).IsRequired(false);
            b.Property(c => c.IsCanceled).HasDefaultValue(false);
        }
    }
}