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


// PaymentCommandHandler
public class PaymentCommandHandler :
    IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
    IRequestHandler<UpdatePaymentCommand, ApiResponse>,
    IRequestHandler<DeletePaymentCommand, ApiResponse>
{
    private readonly AkbankDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCommandHandler(AkbankDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<PaymentRequest, Payment>(request.Model);
        // İlgili işlemleri yapabilirsiniz (örneğin ID ataması, vb.).

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Payment, PaymentResponse>(entityResult.Entity);
        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // Gerekli güncelleme işlemlerini yapabilirsiniz.
        fromdb.PaymentAmount = request.Model.PaymentAmount;
        fromdb.PaymentDate = request.Model.PaymentDate;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.Id == request.Id)
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
