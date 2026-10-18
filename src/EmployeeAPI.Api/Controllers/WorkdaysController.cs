using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Application.Services;
using EmployeeAPI.Application.DTOs;

namespace EmployeeAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkdaysController : ControllerBase
{
    private readonly IWorkdayService _workdayService;

    public WorkdaysController(IWorkdayService workdayService)
    {
        _workdayService = workdayService;
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<WorkdayDto>>> GetWorkdaysByEmployeeId(int employeeId)
    {
        var workdays = await _workdayService.GetWorkdaysByEmployeeIdAsync(employeeId);
        return Ok(workdays);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkdayDto>> GetWorkdayById(int id)
    {
        var workday = await _workdayService.GetWorkdayByIdAsync(id);
        if (workday == null)
            return NotFound();

        return Ok(workday);
    }

    [HttpPost]
    public async Task<ActionResult<WorkdayDto>> CreateWorkday(CreateWorkdayDto createWorkdayDto)
    {
        try
        {
            var workday = await _workdayService.CreateWorkdayAsync(createWorkdayDto);
            return CreatedAtAction(nameof(GetWorkdayById), new { id = workday.Id }, workday);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkday(int id, UpdateWorkdayDto updateWorkdayDto)
    {
        var result = await _workdayService.UpdateWorkdayAsync(id, updateWorkdayDto);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkday(int id)
    {
        var result = await _workdayService.DeleteWorkdayAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
