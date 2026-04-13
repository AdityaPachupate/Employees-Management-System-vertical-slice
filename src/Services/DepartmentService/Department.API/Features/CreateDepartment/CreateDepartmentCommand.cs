using BuildingBlocks.CQRS;

namespace Department.API.Features.CreateDepartment;

public record CreateDepartmentCommand(CreateDepartmentRequest Request) : ICommand<CreateDepartmentResponse>;
