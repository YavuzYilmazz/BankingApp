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

public class PersonelCommandHandler :
    IRequestHandler<CreatePersonelCommand, ApiResponse<PersonelResponse>>,
    IRequestHandler<UpdatePersonelCommand, ApiResponse>,
    IRequestHandler<DeletePersonelCommand, ApiResponse>
{
    private readonly AkbankDbContext dbContext;
    private readonly IMapper mapper;

    public PersonelCommandHandler(AkbankDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PersonelResponse>> Handle(CreatePersonelCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<PersonelRequest, Personel>(request.Model);
        // operations (e.g. ID assignment, etc.).


        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Personel, PersonelResponse>(entityResult.Entity);
        return new ApiResponse<PersonelResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePersonelCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Personel>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // update.
        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.IBAN = request.Model.IBAN;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePersonelCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Personel>().Where(x => x.Id == request.Id)
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
