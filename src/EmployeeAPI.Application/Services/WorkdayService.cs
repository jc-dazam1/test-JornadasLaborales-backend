using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Application.Repositories;
using EmployeeAPI.Domain.Entities;

namespace EmployeeAPI.Application.Services;

public class WorkdayService : IWorkdayService
{
    private readonly IWorkdayRepository _workdayRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public WorkdayService(IWorkdayRepository workdayRepository, IEmployeeRepository employeeRepository)
    {
        _workdayRepository = workdayRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<WorkdayDto>> GetWorkdaysByEmployeeIdAsync(int employeeId)
    {
        var workdays = await _workdayRepository.GetByEmployeeIdAsync(employeeId);
        return workdays.Select(MapToDto).ToList();
    }

    public async Task<WorkdayDto?> GetWorkdayByIdAsync(int id)
    {
        var workday = await _workdayRepository.GetByIdAsync(id);
        return workday != null ? MapToDto(workday) : null;
    }

    public async Task<WorkdayDto> CreateWorkdayAsync(CreateWorkdayDto createWorkdayDto)
    {
        // Verify employee exists
        var employee = await _employeeRepository.GetByIdAsync(createWorkdayDto.EmployeeId);
        if (employee == null)
            throw new ArgumentException($"Employee with ID {createWorkdayDto.EmployeeId} not found");

        var workday = new Workday
        {
            EmployeeId = createWorkdayDto.EmployeeId,
            Date = createWorkdayDto.Date,
            HoursWorked = createWorkdayDto.HoursWorked
        };

        var createdWorkday = await _workdayRepository.CreateAsync(workday);
        return MapToDto(createdWorkday);
    }

    public async Task<bool> UpdateWorkdayAsync(int id, UpdateWorkdayDto updateWorkdayDto)
    {
        var workday = await _workdayRepository.GetByIdAsync(id);
        if (workday == null)
            return false;

        if (updateWorkdayDto.Date.HasValue)
            workday.Date = updateWorkdayDto.Date.Value;
        if (updateWorkdayDto.HoursWorked.HasValue)
            workday.HoursWorked = updateWorkdayDto.HoursWorked.Value;

        return await _workdayRepository.UpdateAsync(workday);
    }

    public async Task<bool> DeleteWorkdayAsync(int id)
    {
        return await _workdayRepository.DeleteAsync(id);
    }

    private static WorkdayDto MapToDto(Workday workday)
    {
        return new WorkdayDto
        {
            Id = workday.Id,
            EmployeeId = workday.EmployeeId,
            Date = workday.Date,
            HoursWorked = workday.HoursWorked
        };
    }
}
