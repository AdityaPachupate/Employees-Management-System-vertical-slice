namespace Department.API.Features.UpdateDepartment;

public record UpdateDepartmentRequest(int DepartmentId, string Name, string? Description);
