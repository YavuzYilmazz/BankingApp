using Akbank.Base.Response;
using Akbank.Base.Schema;
using Akbank.Data.Entity;
using MediatR;

namespace Akbank.Business.Cqrs;

public record CreateExpenseRequestCommand(ExpenseRequestRequest Model) : IRequest<ApiResponse<ExpenseRequestResponse>>;
public record UpdateExpenseRequestCommand(int Id, ExpenseRequestRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseRequestCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseRequestQuery() : IRequest<ApiResponse<List<ExpenseRequestResponse>>>;
public record GetExpenseRequestByIdQuery(int Id) : IRequest<ApiResponse<ExpenseRequestResponse>>;
public record GetExpenseRequestByParameterQuery(decimal RequestAmount, bool Approved) : IRequest<ApiResponse<List<ExpenseRequestResponse>>>;