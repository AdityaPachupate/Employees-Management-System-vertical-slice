using BuildingBlocks.CQRS;

namespace Department.API.Features.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IQuery<GetDepartmentByIdResponse>;
