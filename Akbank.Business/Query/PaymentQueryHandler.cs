using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Akbank.Base.Response;
using Akbank.Business.Cqrs;
using Akbank.Data;
using Akbank.Data.DbContext;
using Akbank.Data.Entity;
using Akbank.Schema;

namespace Akbank.Business.Query
{
    public class PaymentQueryHandler :
        IRequestHandler<GetAllPaymentQuery, ApiResponse<List<PaymentResponse>>>,
        IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>,
        IRequestHandler<GetPaymentByParameterQuery, ApiResponse<List<PaymentResponse>>>
    {
        private readonly AkbankDbContext dbContext;
        private readonly IMapper mapper;

        public PaymentQueryHandler(AkbankDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Payment>()
                .Include(x => x.ExpenseRequest)
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Payment>()
                .Include(x => x.ExpenseRequest)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<PaymentResponse>("Record not found");
            }

            var mapped = mapper.Map<Payment, PaymentResponse>(entity);
            return new ApiResponse<PaymentResponse>(mapped);
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentByParameterQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Payment, bool>> filter = u =>
                (u.PaymentAmount == request.PaymentAmount);


            var list = await dbContext.Set<Payment>()
                .Include(x => x.ExpenseRequest)
                .Where(filter).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }
    }
}
