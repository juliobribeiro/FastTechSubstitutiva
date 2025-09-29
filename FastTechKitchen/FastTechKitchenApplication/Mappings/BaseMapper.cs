using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Application.Mappings;

public class BaseMapper : Profile
{
    public BaseMapper()
    {
        CreateMap<BaseModel, BaseEntity>()
            .ForMember(dest => dest.Id, opt =>
            {
                opt.Condition(src => src.Id.HasValue == false || src.Id.Value == default);
                opt.UseDestinationValue();
            })
            .ForMember(dest => dest.Id, opt =>
            {
                opt.Condition(src => src.Id.HasValue == true && src.Id.Value != default);
                opt.MapFrom(src => src.Id);
            });
    }
}
