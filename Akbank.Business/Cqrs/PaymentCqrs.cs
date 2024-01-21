using Akbank.Base.Response;
using Akbank.Base.Schema;
using Akbank.Schema;
using MediatR;

namespace Akbank.Business.Cqrs;

public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;
public record UpdatePaymentCommand(int Id, PaymentRequest Model) : IRequest<ApiResponse>;
public record DeletePaymentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPaymentQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;
public record GetPaymentByParameterQuery(decimal PaymentAmount) : IRequest<ApiResponse<List<PaymentResponse>>>;