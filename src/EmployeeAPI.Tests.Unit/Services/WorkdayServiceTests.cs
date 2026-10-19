using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Application.Repositories;
using EmployeeAPI.Application.Services;
using EmployeeAPI.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EmployeeAPI.Tests.Unit.Services;

public class WorkdayServiceTests
{
    private readonly Mock<IWorkdayRepository> _mockWorkdayRepository;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
    private readonly IWorkdayService _workdayService;

    public WorkdayServiceTests()
    {
        _mockWorkdayRepository = new Mock<IWorkdayRepository>();
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _workdayService = new WorkdayService(_mockWorkdayRepository.Object, _mockEmployeeRepository.Object);
    }

    [Fact]
    public async Task GetWorkdaysByEmployeeIdAsync_WithValidEmployeeId_ReturnsWorkdaysList()
    {
        // Arrange
        var workdays = new List<Workday>
        {
            new Workday { Id = 1, EmployeeId = 1, Date = DateTime.Now, HoursWorked = 8 },
            new Workday { Id = 2, EmployeeId = 1, Date = DateTime.Now.AddDays(-1), HoursWorked = 8 }
        };

        _mockWorkdayRepository.Setup(r => r.GetByEmployeeIdAsync(1))
            .ReturnsAsync(workdays);

        // Act
        var result = await _workdayService.GetWorkdaysByEmployeeIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().EmployeeId.Should().Be(1);
    }

    [Fact]
    public async Task GetWorkdayByIdAsync_WithValidId_ReturnsWorkday()
    {
        // Arrange
        var workday = new Workday { Id = 1, EmployeeId = 1, Date = DateTime.Now, HoursWorked = 8 };

        _mockWorkdayRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(workday);

        // Act
        var result = await _workdayService.GetWorkdayByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.HoursWorked.Should().Be(8);
    }

    [Fact]
    public async Task CreateWorkdayAsync_WithValidData_CreatesWorkday()
    {
        // Arrange
        var createDto = new CreateWorkdayDto
        {
            EmployeeId = 1,
            Date = DateTime.Now,
            HoursWorked = 8.5m
        };

        var employee = new Employee { Id = 1, FirstName = "Juan", LastName = "Pérez", Email = "juan@example.com", Position = "Developer" };
        var createdWorkday = new Workday { Id = 1, EmployeeId = 1, Date = createDto.Date, HoursWorked = createDto.HoursWorked };

        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(employee);

        _mockWorkdayRepository.Setup(r => r.CreateAsync(It.IsAny<Workday>()))
            .ReturnsAsync(createdWorkday);

        // Act
        var result = await _workdayService.CreateWorkdayAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result!.EmployeeId.Should().Be(1);
        result.HoursWorked.Should().Be(8.5m);
    }

    [Fact]
    public async Task CreateWorkdayAsync_WithInvalidEmployeeId_ThrowsException()
    {
        // Arrange
        var createDto = new CreateWorkdayDto
        {
            EmployeeId = 999,
            Date = DateTime.Now,
            HoursWorked = 8
        };

        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Employee?)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _workdayService.CreateWorkdayAsync(createDto));
    }

    [Fact]
    public async Task UpdateWorkdayAsync_WithValidData_UpdatesWorkday()
    {
        // Arrange
        var existingWorkday = new Workday { Id = 1, EmployeeId = 1, Date = DateTime.Now.AddDays(-1), HoursWorked = 8 };

        var updateDto = new UpdateWorkdayDto
        {
            Date = DateTime.Now,
            HoursWorked = 7.5m
        };

        _mockWorkdayRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existingWorkday);

        _mockWorkdayRepository.Setup(r => r.UpdateAsync(It.IsAny<Workday>()))
            .ReturnsAsync(true);

        // Act
        var result = await _workdayService.UpdateWorkdayAsync(1, updateDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteWorkdayAsync_WithValidId_DeletesWorkday()
    {
        // Arrange
        _mockWorkdayRepository.Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        // Act
        var result = await _workdayService.DeleteWorkdayAsync(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteWorkdayAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _mockWorkdayRepository.Setup(r => r.DeleteAsync(999))
            .ReturnsAsync(false);

        // Act
        var result = await _workdayService.DeleteWorkdayAsync(999);

        // Assert
        result.Should().BeFalse();
    }
}
