using Akbank.Base.Response;
using Akbank.Base.Schema;
using Akbank.Schema;
using MediatR;

namespace Akbank.Business.Cqrs;

// ExpenseCategory CQRS
public record CreateExpenseCategoryCommand(ExpenseCategoryRequest Model) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
public record UpdateExpenseCategoryCommand(int Id, ExpenseCategoryRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenseCategoryCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenseCategoryQuery() : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>;
public record GetExpenseCategoryByIdQuery(int Id) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
public record GetExpenseCategoryByParameterQuery(string CategoryName) : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>;