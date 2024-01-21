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
    public class UserQueryHandler :
        IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
        IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>,
        IRequestHandler<GetUserByParameterQuery, ApiResponse<List<UserResponse>>>
    {
        private readonly AkbankDbContext dbContext;
        private readonly IMapper mapper;

        public UserQueryHandler(AkbankDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<User>()
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
            return new ApiResponse<List<UserResponse>>(mappedList);
        }

        public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<UserResponse>("Record not found");
            }

            var mapped = mapper.Map<User, UserResponse>(entity);
            return new ApiResponse<UserResponse>(mapped);
        }

        public async Task<ApiResponse<List<UserResponse>>> Handle(GetUserByParameterQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> filter = u =>
                (u.UserName == request.UserName);


            var list = await dbContext.Set<User>()
                .Where(filter).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
            return new ApiResponse<List<UserResponse>>(mappedList);
        }
    }
}
