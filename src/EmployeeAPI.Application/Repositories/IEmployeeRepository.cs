using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Application.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}

public interface IWorkdayRepository
{
    Task<IEnumerable<Workday>> GetByEmployeeIdAsync(int id);
    Task<Workday?> GetByIdAsync(int id);
    Task<Workday> CreateAsync(Workday workday);
    Task<bool> UpdateAsync(Workday workday);
    Task<bool> DeleteAsync(int id);
}
