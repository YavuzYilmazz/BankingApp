using Akbank.Base.Response;
using Akbank.Base.Schema;
using Akbank.Schema;
using MediatR;

namespace Akbank.Business.Cqrs;

public record CreateUserCommand(UserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, UserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
public record GetUserByParameterQuery(string UserName) : IRequest<ApiResponse<List<UserResponse>>>;

