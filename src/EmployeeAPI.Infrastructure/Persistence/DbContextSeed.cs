using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Infrastructure.Persistence;

public static class DbContextSeed
{
    public static void Initialize(EmployeeDbContext context)
    {
        // Don't add seed data if it already exists
        if (context.Employees.Any())
            return;

        var employees = new List<Employee>
        {
            new Employee
            {
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                Position = "Senior Developer",
                CreatedAt = DateTime.UtcNow,
                Workdays = new List<Workday>
                {
                    new Workday { Date = DateTime.UtcNow.AddDays(-5), HoursWorked = 8m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-4), HoursWorked = 8m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-3), HoursWorked = 8m }
                }
            },
            new Employee
            {
                FirstName = "María",
                LastName = "García",
                Email = "maria.garcia@example.com",
                Position = "Product Manager",
                CreatedAt = DateTime.UtcNow,
                Workdays = new List<Workday>
                {
                    new Workday { Date = DateTime.UtcNow.AddDays(-5), HoursWorked = 8m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-4), HoursWorked = 7m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-3), HoursWorked = 8m }
                }
            },
            new Employee
            {
                FirstName = "Carlos",
                LastName = "López",
                Email = "carlos.lopez@example.com",
                Position = "QA Engineer",
                CreatedAt = DateTime.UtcNow,
                Workdays = new List<Workday>
                {
                    new Workday { Date = DateTime.UtcNow.AddDays(-5), HoursWorked = 8m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-4), HoursWorked = 8m },
                    new Workday { Date = DateTime.UtcNow.AddDays(-3), HoursWorked = 8m }
                }
            }
        };

        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}
