

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
    public class PersonelQueryHandler :
        IRequestHandler<GetAllPersonelQuery, ApiResponse<List<PersonelResponse>>>,
        IRequestHandler<GetPersonelByIdQuery, ApiResponse<PersonelResponse>>,
        IRequestHandler<GetPersonelByParameterQuery, ApiResponse<List<PersonelResponse>>>
    {
        private readonly AkbankDbContext dbContext;
        private readonly IMapper mapper;

        public PersonelQueryHandler(AkbankDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<PersonelResponse>>> Handle(GetAllPersonelQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Personel>()
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Personel>, List<PersonelResponse>>(list);
            return new ApiResponse<List<PersonelResponse>>(mappedList);
        }

        public async Task<ApiResponse<PersonelResponse>> Handle(GetPersonelByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Personel>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<PersonelResponse>("Record not found");
            }

            var mapped = mapper.Map<Personel, PersonelResponse>(entity);
            return new ApiResponse<PersonelResponse>(mapped);
        }

        public async Task<ApiResponse<List<PersonelResponse>>> Handle(GetPersonelByParameterQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Personel, bool>> filter = u =>
                (u.FirstName == request.FirstName) &&
                (u.LastName == request.LastName);


            var list = await dbContext.Set<Personel>()
                .Where(filter).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Personel>, List<PersonelResponse>>(list);
            return new ApiResponse<List<PersonelResponse>>(mappedList);
        }
    }
}
