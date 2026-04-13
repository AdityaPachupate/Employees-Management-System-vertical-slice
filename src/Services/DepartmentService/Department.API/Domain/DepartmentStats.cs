using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Department.API.Domain;

public class DepartmentStats
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DepartmentId { get; set; }

    public int EmployeeCount { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
