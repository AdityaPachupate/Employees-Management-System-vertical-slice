namespace Department.API.Features.GetDepartments;

public record DepartmentDto(
    int DepartmentId, 
    string Name, 
    string? Description, 
    DateTime CreatedDate, 
    int EmployeeCount);

public record GetDepartmentsResponse(IEnumerable<DepartmentDto> Departments);
