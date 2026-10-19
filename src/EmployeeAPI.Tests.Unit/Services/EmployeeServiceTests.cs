using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Application.Repositories;
using EmployeeAPI.Application.Services;
using EmployeeAPI.Domain.Entities;
using FluentAssertions;
using Moq;

namespace EmployeeAPI.Tests.Unit.Services;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
    private readonly Mock<IWorkdayRepository> _mockWorkdayRepository;
    private readonly IEmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _mockWorkdayRepository = new Mock<IWorkdayRepository>();
        _employeeService = new EmployeeService(_mockEmployeeRepository.Object, _mockWorkdayRepository.Object);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WithEmployees_ReturnsListOfEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Juan", LastName = "Pérez", Email = "juan@example.com", Position = "Developer" },
            new Employee { Id = 2, FirstName = "María", LastName = "García", Email = "maria@example.com", Position = "Manager" }
        };

        _mockEmployeeRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(employees);

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().FirstName.Should().Be("Juan");
        _mockEmployeeRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WithNoEmployees_ReturnsEmptyList()
    {
        // Arrange
        _mockEmployeeRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Employee>());

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_WithValidId_ReturnsEmployee()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            FirstName = "Juan",
            LastName = "Pérez",
            Email = "juan@example.com",
            Position = "Developer"
        };

        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(employee);

        // Act
        var result = await _employeeService.GetEmployeeByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.FirstName.Should().Be("Juan");
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Employee?)null);

        // Act
        var result = await _employeeService.GetEmployeeByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithValidData_CreatesAndReturnsEmployee()
    {
        // Arrange
        var createDto = new CreateEmployeeDto
        {
            FirstName = "Carlos",
            LastName = "López",
            Email = "carlos@example.com",
            Position = "Designer"
        };

        var createdEmployee = new Employee
        {
            Id = 3,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            Position = createDto.Position
        };

        _mockEmployeeRepository.Setup(r => r.CreateAsync(It.IsAny<Employee>()))
            .ReturnsAsync(createdEmployee);

        // Act
        var result = await _employeeService.CreateEmployeeAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("Carlos");
        result.Email.Should().Be("carlos@example.com");
        _mockEmployeeRepository.Verify(r => r.CreateAsync(It.IsAny<Employee>()), Times.Once);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_WithValidData_UpdatesEmployee()
    {
        // Arrange
        var updateDto = new UpdateEmployeeDto
        {
            FirstName = "Carlos Updated",
            LastName = "López García",
            Email = "carlos.updated@example.com",
            Position = "Senior Designer"
        };

        var existingEmployee = new Employee
        {
            Id = 3,
            FirstName = "Carlos",
            LastName = "López",
            Email = "carlos@example.com",
            Position = "Designer"
        };

        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(3))
            .ReturnsAsync(existingEmployee);

        _mockEmployeeRepository.Setup(r => r.UpdateAsync(It.IsAny<Employee>()))
            .ReturnsAsync(true);

        // Act
        var result = await _employeeService.UpdateEmployeeAsync(3, updateDto);

        // Assert
        result.Should().BeTrue();
        _mockEmployeeRepository.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WithValidId_DeletesEmployee()
    {
        // Arrange
        _mockEmployeeRepository.Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(1);

        // Assert
        result.Should().BeTrue();
        _mockEmployeeRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _mockEmployeeRepository.Setup(r => r.DeleteAsync(999))
            .ReturnsAsync(false);

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetEmployeeWorkdaysAsync_WithValidId_ReturnsWorkdaysDto()
    {
        // Arrange
        var employee = new Employee
        {
            Id = 1,
            FirstName = "Juan",
            LastName = "Pérez",
            Email = "juan@example.com",
            Position = "Developer"
        };

        var workdays = new List<Workday>
        {
            new Workday { Id = 1, EmployeeId = 1, Date = DateTime.Now, HoursWorked = 8 },
            new Workday { Id = 2, EmployeeId = 1, Date = DateTime.Now.AddDays(-1), HoursWorked = 8 }
        };

        _mockEmployeeRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(employee);

        _mockWorkdayRepository.Setup(r => r.GetByEmployeeIdAsync(1))
            .ReturnsAsync(workdays);

        // Act
        var result = await _employeeService.GetEmployeeWorkdaysAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.EmployeeId.Should().Be(1);
    }
}
