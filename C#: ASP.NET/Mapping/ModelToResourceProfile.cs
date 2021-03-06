using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;
using Supermarket.API.Extensions;
using Supermarket.API.Resources;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Employee, EmployeeResource>()
                .ForMember(src => src.UnitOfProblems,
                           opt => opt.MapFrom(src => src.UnitOfProblems.ToDescriptionString()));

            CreateMap<QueryResult<Employee>, QueryResultResource<EmployeeResource>>();
            
            CreateMap<Problem, ProblemResource>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => src.UnitOfProblems.ToDescriptionString()));

            CreateMap<QueryResult<Problem>, QueryResultResource<ProblemResource>>();
        }
    }
}