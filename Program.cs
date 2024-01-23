using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Employees.Service;
using EmployeeLogs.Service;
using Database.Employee;
using Microsoft.EntityFrameworkCore;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<EmployeesService>();
        services.AddSingleton<EmployeeLogsService>(provider =>
    {
        var employeesService = provider.GetRequiredService<EmployeesService>();
        return new EmployeeLogsService(employeesService);
    });
        services.AddDbContext<EmployeesDb>(options => {
            options.UseSqlServer("Server=tcp:cdvnhserver.database.windows.net,1433;Initial Catalog=CDV-2023;Persist Security Info=False;User ID=natalia;Password=#cdvnhserver2001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        });
    })
    .Build();

host.Run();
