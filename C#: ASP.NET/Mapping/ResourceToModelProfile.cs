using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Resources;

namespace Supermarket.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();

            CreateMap<SaveProductResource, Employee>()
                .ForMember(src => src.UnitOfProblems, opt => opt.MapFrom(src => (TypesOfProblems)TypesOfProblems.Open));

            CreateMap<ProductsQueryResource, ProductsQuery>();
            
            CreateMap<SaveProblemResource, Problem>()
                .ForMember(src => src.UnitOfProblems, opt => opt.MapFrom(src => (TypesOfProblems)src.UnitOfMeasurement));

            CreateMap<ProblemsQueryResource, ProblemsQuery>();
        }
    }
}