using Department.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Data;

public class DepartmentDbContext : DbContext
{
    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Department> Departments => Set<Domain.Department>();
    public DbSet<DepartmentStats> DepartmentStats => Set<DepartmentStats>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<DepartmentStats>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);
            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
        });
    }
}
