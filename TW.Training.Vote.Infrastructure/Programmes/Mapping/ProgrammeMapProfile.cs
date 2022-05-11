using AutoMapper;
using TW.Training.Vote.Domain.Programmes;

namespace TW.Training.Vote.Infrastructure.Programmes.Mapping;

public class ProgrammeMapProfile : Profile
{
    public ProgrammeMapProfile()
    {
        CreateMap<Programme, PO.Programme>().ReverseMap();
        CreateMap<ProgrammeItem, PO.ProgrammeItem>().ReverseMap();
    }
}