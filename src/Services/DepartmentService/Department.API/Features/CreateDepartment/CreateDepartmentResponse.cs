namespace Department.API.Features.CreateDepartment;

public record CreateDepartmentResponse(int DepartmentId, string Name, string? Description, DateTime CreatedDate);
