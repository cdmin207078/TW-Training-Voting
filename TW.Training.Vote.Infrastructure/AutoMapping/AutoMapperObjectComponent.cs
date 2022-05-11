using AutoMapper;
using TW.Infrastructure.Core.Components;

namespace TW.Training.Vote.Infrastructure.AutoMapping;

public class AutoMapperObjectComponent : IObjectMapperComponent
{
    private readonly IMapper _mapper;

    public AutoMapperObjectComponent(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => _mapper.Map<TSource, TDestination>(source, destination);
}