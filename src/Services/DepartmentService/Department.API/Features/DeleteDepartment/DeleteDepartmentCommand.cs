using BuildingBlocks.CQRS;

namespace Department.API.Features.DeleteDepartment;

public record DeleteDepartmentCommand(int Id) : ICommand<DeleteDepartmentResponse>;
