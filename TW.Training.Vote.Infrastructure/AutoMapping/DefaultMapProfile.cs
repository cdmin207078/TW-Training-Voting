using AutoMapper;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Infrastructure.AutoMapping;

public class DefaultMapProfile : Profile
{
    public DefaultMapProfile()
    {
        CreateMap<CodeNumber, string>()
            .ConstructUsing(source => source ==  null ? null : source.Value)
            .ReverseMap()
            .ConstructUsing(source => string.IsNullOrWhiteSpace(source) ? null : new CodeNumber(source));
        
        CreateMap<MobilePhoneNumber, string>()
            .ConstructUsing(source => source == null ? null : source.Value)
            .ReverseMap()
            .ConstructUsing(source => string.IsNullOrWhiteSpace(source) ? null : new MobilePhoneNumber(source));
        
        CreateMap<Id<int>, int>()
            .ConstructUsing(source => source.Value)
            .ReverseMap()
            .ConstructUsing(source => source < 1 ? null : new Id<int>(source));

        CreateMap<Id<int>, int?>()
            .ConstructUsing(source => source == null ? null : source.Value)
            .ReverseMap()
            .ConstructUsing(source => source.HasValue ? new Id<int>(source.Value) : null);
    }
}
