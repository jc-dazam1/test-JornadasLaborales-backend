namespace EmployeeAPI.Domain.Entities;

public class Workday
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public decimal HoursWorked { get; set; }

    public Employee Employee { get; set; } = null!;
}
