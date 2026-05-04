namespace Department.API.Features.GetDepartmentById;

public record GetDepartmentByIdResponse(
    int DepartmentId, 
    string Name, 
    string? Description, 
    DateTime CreatedDate, 
    int EmployeeCount);
