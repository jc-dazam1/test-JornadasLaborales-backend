using EmployeeAPI.Application.DTOs;

namespace EmployeeAPI.Application.Services;

public interface IWorkdayService
{
    Task<IEnumerable<WorkdayDto>> GetWorkdaysByEmployeeIdAsync(int employeeId);
    Task<WorkdayDto?> GetWorkdayByIdAsync(int id);
    Task<WorkdayDto> CreateWorkdayAsync(CreateWorkdayDto createWorkdayDto);
    Task<bool> UpdateWorkdayAsync(int id, UpdateWorkdayDto updateWorkdayDto);
    Task<bool> DeleteWorkdayAsync(int id);
}
