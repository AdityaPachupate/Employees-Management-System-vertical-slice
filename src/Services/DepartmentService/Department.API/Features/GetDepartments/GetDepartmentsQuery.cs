using BuildingBlocks.CQRS;

namespace Department.API.Features.GetDepartments;

public record GetDepartmentsQuery() : IQuery<GetDepartmentsResponse>;
