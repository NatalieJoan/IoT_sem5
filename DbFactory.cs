using Database.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.Factory
{
    public class EmployeesDbFactory : IDesignTimeDbContextFactory<EmployeesDb>
        {
            public EmployeesDb CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EmployeesDb>();
                optionsBuilder.UseSqlServer("Server=tcp:cdvnhserver.database.windows.net,1433;Initial Catalog=CDV-2023;Persist Security Info=False;User ID=natalia;Password=#cdvnhserver2001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                return new EmployeesDb(optionsBuilder.Options);
            }
        }
    }