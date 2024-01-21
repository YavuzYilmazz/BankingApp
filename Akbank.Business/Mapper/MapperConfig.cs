using AutoMapper;
using Akbank.Data.Entity;
using Akbank.Schema;

namespace Akbank.Business.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PersonelRequest, Personel>();
            CreateMap<Personel, PersonelResponse>();

            CreateMap<ExpenseRequestRequest, ExpenseRequest>();
            CreateMap<ExpenseRequest, ExpenseRequestResponse>();
                

            CreateMap<ExpenseCategoryRequest, ExpenseCategory>();
            CreateMap<ExpenseCategory, ExpenseCategoryResponse>();

            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<PaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>();
        }
    }
}