using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Akbank.Base.Response;
using Akbank.Business.Cqrs;
using Akbank.Data;
using Akbank.Data.DbContext;
using Akbank.Data.Entity;
using Akbank.Schema;

namespace Akbank.Business.Command;

public class ExpenseRequestCommandHandler :
    IRequestHandler<CreateExpenseRequestCommand, ApiResponse<ExpenseRequestResponse>>,
    IRequestHandler<UpdateExpenseRequestCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseRequestCommand, ApiResponse>
{
    private readonly AkbankDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseRequestCommandHandler(AkbankDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseRequestResponse>> Handle(CreateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseRequestRequest, ExpenseRequest>(request.Model);
        // İlgili işlemleri yapabilirsiniz (örneğin ID ataması, vb.).

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenseRequest, ExpenseRequestResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseRequestResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // Gerekli güncelleme işlemlerini yapabilirsiniz.
        fromdb.RequestAmount = request.Model.RequestAmount;
        fromdb.Description = request.Model.Description;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteExpenseRequestCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenseRequest>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // Gerekli silme işlemlerini yapabilirsiniz.
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
