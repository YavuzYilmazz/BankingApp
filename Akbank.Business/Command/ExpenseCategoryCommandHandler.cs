using MediatR;
using Microsoft.EntityFrameworkCore;
using Akbank.Base.Response;
using Akbank.Business.Cqrs;
using Akbank.Data;
using Akbank.Data.DbContext;
using Akbank.Data.Entity;
using Akbank.Schema;
using AutoMapper;

namespace Akbank.Business.Command;

public class ExpenseCategoryCommandHandler :
    IRequestHandler<CreateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>,
    IRequestHandler<UpdateExpenseCategoryCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
{
    private readonly AkbankDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseCategoryCommandHandler(AkbankDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseCategoryRequest, ExpenseCategory>(request.Model);
        // operations (e.g. ID assignment, etc.).

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseCategoryResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseCategory>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // update.
        fromdb.CategoryName = request.Model.CategoryName;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseCategory>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // delete.
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
