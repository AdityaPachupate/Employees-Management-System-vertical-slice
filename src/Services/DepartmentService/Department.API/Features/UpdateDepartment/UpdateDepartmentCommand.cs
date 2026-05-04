using BuildingBlocks.CQRS;

namespace Department.API.Features.UpdateDepartment;

public record UpdateDepartmentCommand(UpdateDepartmentRequest Request) : ICommand<UpdateDepartmentResponse>;
