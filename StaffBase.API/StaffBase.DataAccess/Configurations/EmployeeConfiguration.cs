using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffBase.DataAccess.Entities;

namespace StaffBase.DataAccess.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder
                .HasOne(x => x.Organization)
                .WithMany(y => y.Employee)
                .HasForeignKey(x => x.OrganizationId);
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.PassportSeries).IsRequired();
            builder.Property(e => e.PassportNumber).IsRequired();
            builder.Property(e => e.Firstname).IsRequired();
            builder.Property(e => e.Patronomic).IsRequired();
            builder.Property(e => e.Birthdate).IsRequired();
            builder.Property(e => e.Lastname).IsRequired();
            builder.Property(e => e.OrganizationId).IsRequired();
            builder.Property(e => e.PassportNumber).HasMaxLength(StaffBase.Core.Models.Employee.PASPORT_NUMBER_SIZE);
            builder.Property(e => e.PassportSeries).HasMaxLength(StaffBase.Core.Models.Employee.PASPORT_SERIES_SIZE);
            builder.Property(e => e.Firstname).HasMaxLength(StaffBase.Core.Models.Employee.MAX_NAMES_SIZE);
            builder.Property(e => e.Patronomic).HasMaxLength(StaffBase.Core.Models.Employee.MAX_NAMES_SIZE);
            builder.Property(e => e.Lastname).HasMaxLength(StaffBase.Core.Models.Employee.MAX_NAMES_SIZE);
        }
    }
}
