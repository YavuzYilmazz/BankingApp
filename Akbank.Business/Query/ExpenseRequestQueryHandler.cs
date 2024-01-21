

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
    public class ExpenseRequestQueryHandler :
        IRequestHandler<GetAllExpenseRequestQuery, ApiResponse<List<ExpenseRequestResponse>>>,
        IRequestHandler<GetExpenseRequestByIdQuery, ApiResponse<ExpenseRequestResponse>>,
        IRequestHandler<GetExpenseRequestByParameterQuery, ApiResponse<List<ExpenseRequestResponse>>>
    {
        private readonly AkbankDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseRequestQueryHandler(AkbankDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetAllExpenseRequestQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<ExpenseRequest>()
                .Include(x => x.Personel)
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Payments)
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseRequestResponse>>(list);
            return new ApiResponse<List<ExpenseRequestResponse>>(mappedList);
        }

        public async Task<ApiResponse<ExpenseRequestResponse>> Handle(GetExpenseRequestByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ExpenseRequest>()
                .Include(x => x.Personel)
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Payments)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<ExpenseRequestResponse>("Record not found");
            }

            var mapped = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(entity);
            return new ApiResponse<ExpenseRequestResponse>(mapped);
        }

        public async Task<ApiResponse<List<ExpenseRequestResponse>>> Handle(GetExpenseRequestByParameterQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<ExpenseRequest, bool>> filter = u =>
                (u.RequestAmount == request.RequestAmount) &&
                (u.Approved == request.Approved);


            var list = await dbContext.Set<ExpenseRequest>()
                .Include(x => x.Personel)
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Payments)
                .Where(filter).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<ExpenseRequest>, List<ExpenseRequestResponse>>(list);
            return new ApiResponse<List<ExpenseRequestResponse>>(mappedList);
        }
    }
}
