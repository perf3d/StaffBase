
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffBase.DataAccess.Entities;
using System.Security.Cryptography.X509Certificates;

namespace StaffBase.DataAccess.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<OrganizationEntity>
    {
        public void Configure(EntityTypeBuilder<OrganizationEntity> builder)
        {
            builder.HasKey(x => x.Id);
            //builder
            //    .HasMany(x => x.Employee)
            //    .WithOne(y => y.Organization)
            //    .HasForeignKey(x => x.OrganizationId);
            builder.Property(x => x.ActualAddress).IsRequired();
            builder.Property(x => x.Inn).IsRequired();
            builder.Property(x => x.LegalAddress).IsRequired();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(e => e.Inn).HasMaxLength(StaffBase.Core.Models.Organization.INN_MAX_SIZE);
            builder.Property(e => e.Name).HasMaxLength(StaffBase.Core.Models.Organization.NAME_MAX_SIZE);
        }
    }
}
