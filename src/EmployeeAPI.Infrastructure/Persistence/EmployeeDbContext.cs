using EmployeeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Infrastructure.Persistence;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Workday> Workdays { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Employee entity
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasMany(e => e.Workdays).WithOne(w => w.Employee).HasForeignKey(w => w.EmployeeId);
        });

        // Configure Workday entity
        modelBuilder.Entity<Workday>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Date).IsRequired();
            entity.Property(w => w.HoursWorked).IsRequired();
            entity.HasOne(w => w.Employee).WithMany(e => e.Workdays).HasForeignKey(w => w.EmployeeId);
        });
    }
}
