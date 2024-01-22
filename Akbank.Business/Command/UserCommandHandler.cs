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

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand, ApiResponse>,
    IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly AkbankDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(AkbankDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<UserRequest, User>(request.Model);
        // operations (e.g. ID assignment, etc.).


        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // update.
        fromdb.UserName = request.Model.UserName;
        fromdb.Password = request.Model.Password;
        fromdb.IsAdmin = request.Model.IsAdmin;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
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
