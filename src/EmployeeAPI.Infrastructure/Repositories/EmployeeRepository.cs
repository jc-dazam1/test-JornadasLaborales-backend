using EmployeeAPI.Application.Repositories;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Infrastructure.Persistence;

namespace EmployeeAPI.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeDbContext _dbContext;

    public EmployeeRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return _dbContext.Employees.ToList();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return _dbContext.Employees.FirstOrDefault(e => e.Id == id);
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> UpdateAsync(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return false;

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}

public class WorkdayRepository : IWorkdayRepository
{
    private readonly EmployeeDbContext _dbContext;

    public WorkdayRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Workday>> GetByEmployeeIdAsync(int id)
    {
        return _dbContext.Workdays.Where(w => w.EmployeeId == id).ToList();
    }
}
