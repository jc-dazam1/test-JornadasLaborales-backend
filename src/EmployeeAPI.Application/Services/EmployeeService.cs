using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Application.Repositories;
using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWorkdayRepository _workdayRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IWorkdayRepository workdayRepository)
    {
        _employeeRepository = employeeRepository;
        _workdayRepository = workdayRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee != null ? MapToDto(employee) : null;
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            Position = createEmployeeDto.Position
        };

        var createdEmployee = await _employeeRepository.CreateAsync(employee);
        return MapToDto(createdEmployee);
    }

    public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return false;

        if (!string.IsNullOrEmpty(updateEmployeeDto.FirstName))
            employee.FirstName = updateEmployeeDto.FirstName;
        if (!string.IsNullOrEmpty(updateEmployeeDto.LastName))
            employee.LastName = updateEmployeeDto.LastName;
        if (!string.IsNullOrEmpty(updateEmployeeDto.Email))
            employee.Email = updateEmployeeDto.Email;
        if (!string.IsNullOrEmpty(updateEmployeeDto.Position))
            employee.Position = updateEmployeeDto.Position;

        return await _employeeRepository.UpdateAsync(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }

    public async Task<EmployeeWorkdaysDto?> GetEmployeeWorkdaysAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return null;

        var workdays = await _workdayRepository.GetByEmployeeIdAsync(id);

        return new EmployeeWorkdaysDto
        {
            EmployeeId = employee.Id,
            EmployeeName = $"{employee.FirstName} {employee.LastName}",
            Workdays = workdays.OrderBy(w => w.Date).Select(w => w.Date).ToList()
        };
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Position = employee.Position
        };
    }
}
