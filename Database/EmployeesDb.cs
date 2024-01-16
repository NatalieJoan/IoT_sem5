using Database.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Employee
{
    public class EmployeesDb : DbContext
    {
        public EmployeesDb(DbContextOptions<EmployeesDb> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployeeEntity(modelBuilder.Entity<EmployeeEntity>());
            base.OnModelCreating(modelBuilder);
        }
        private void ConfigureEmployeeEntity(EntityTypeBuilder<EmployeeEntity> entity)
        {
            entity.ToTable("Employee");
            entity.Property(p => p._firstName).IsRequired().HasMaxLength(200);
            entity.Property(p => p._lastName).IsRequired().HasMaxLength(200);
        }

        public DbSet<EmployeeEntity> Employees {get; set;}
    }
}