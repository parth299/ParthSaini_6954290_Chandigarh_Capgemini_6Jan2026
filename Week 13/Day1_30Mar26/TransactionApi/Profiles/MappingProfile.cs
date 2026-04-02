using AutoMapper;
using TransactionApi.Models;
using TransactionApi.DTOs;

namespace TransactionApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionDto>();

            CreateMap<CreateTransactionDto,
                      Transaction>();
        }
    }
}