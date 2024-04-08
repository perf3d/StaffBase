
using Microsoft.EntityFrameworkCore;
using StaffBase.DataAccess.Entities;

namespace StaffBase.DataAccess
{
    public class StaffBaseDbContext : DbContext
    {
        public StaffBaseDbContext(DbContextOptions<StaffBaseDbContext> options) : base(options) { }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<EmployeeEntity> Employee { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
