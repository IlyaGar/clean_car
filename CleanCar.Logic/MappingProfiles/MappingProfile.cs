using AutoMapper;
using CleanCar.DAL.Models;
using CleanCar.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                 .ForMember(c => c.CountOperations, c => c.MapFrom(src => MapCount(src)))
                 .ForMember(c => c.SumPrice, c => c.MapFrom(src => MapSumPrice(src)));

            CreateMap<Order, OrderDTO>()
                .ForMember(c => c.Name, c => c.MapFrom(src => src.Operation.Name));
        }

        private int MapCount(Customer source) => source.Orders.Select(c => c.Price).ToList().Count;

        private decimal MapSumPrice(Customer source) => source.Orders.Select(c => c.Price).ToList().Sum();
    }
}
