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

    public TDestination Map<TDestination>(object source) => Execute<object, TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source) => Execute<TSource, TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => Execute(source, destination);

    public TDestination Execute<TSource, TDestination>(TSource source)
    {
        try
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
        catch (AutoMapperMappingException ex)
        {
            throw ex.InnerException;
        }
    }
    
    public TDestination Execute<TSource, TDestination>(TSource source, TDestination destination)
    {
        try
        {
            return _mapper.Map<TSource, TDestination>(source, destination);
        }
        catch (AutoMapperMappingException ex)
        {
            throw ex.InnerException;
        }
    }
}