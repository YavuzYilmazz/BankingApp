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
    public class ExpenseCategoryQueryHandler :
        IRequestHandler<GetAllExpenseCategoryQuery, ApiResponse<List<ExpenseCategoryResponse>>>,
        IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>,
        IRequestHandler<GetExpenseCategoryByParameterQuery, ApiResponse<List<ExpenseCategoryResponse>>>
    {
        private readonly AkbankDbContext dbContext;
        private readonly IMapper mapper;

        public ExpenseCategoryQueryHandler(AkbankDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<ExpenseCategory>()
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<ExpenseCategory>, List<ExpenseCategoryResponse>>(list);
            return new ApiResponse<List<ExpenseCategoryResponse>>(mappedList);
        }

        public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ExpenseCategory>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<ExpenseCategoryResponse>("Record not found");
            }

            var mapped = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entity);
            return new ApiResponse<ExpenseCategoryResponse>(mapped);
        }

        public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetExpenseCategoryByParameterQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<ExpenseCategory, bool>> filter = u =>
                (u.CategoryName == request.CategoryName);


            var list = await dbContext.Set<ExpenseCategory>()
                .Where(filter).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<ExpenseCategory>, List<ExpenseCategoryResponse>>(list);
            return new ApiResponse<List<ExpenseCategoryResponse>>(mappedList);
        }
    }
}
