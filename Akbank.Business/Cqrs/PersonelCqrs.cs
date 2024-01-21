using Akbank.Base.Response;
using Akbank.Base.Schema;
using Akbank.Schema;
using MediatR;

namespace Akbank.Business.Cqrs;

public record CreatePersonelCommand(PersonelRequest Model) : IRequest<ApiResponse<PersonelResponse>>;
public record UpdatePersonelCommand(int Id, PersonelRequest Model) : IRequest<ApiResponse>;
public record DeletePersonelCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPersonelQuery() : IRequest<ApiResponse<List<PersonelResponse>>>;
public record GetPersonelByIdQuery(int Id) : IRequest<ApiResponse<PersonelResponse>>;
public record GetPersonelByParameterQuery(string FirstName, string LastName) : IRequest<ApiResponse<List<PersonelResponse>>>;
