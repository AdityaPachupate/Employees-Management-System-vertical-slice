using System.ComponentModel.DataAnnotations;

namespace Department.API.Domain;

public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(250)]
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

}
