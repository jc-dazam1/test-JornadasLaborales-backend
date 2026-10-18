using EmployeeAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Application.Services;
using EmployeeAPI.Application.DTOs;

namespace EmployeeAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
    {
        var employee = await _employeeService.CreateEmployeeAsync(createEmployeeDto);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
    {
        var result = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("workdays/{id}")]
    public async Task<ActionResult<EmployeeWorkdaysDto>> GetEmployeeWorkdays(int id)
    {
        var workdays = await _employeeService.GetEmployeeWorkdaysAsync(id);
        if (workdays == null)
            return NotFound();

        return Ok(workdays);
    }
}
