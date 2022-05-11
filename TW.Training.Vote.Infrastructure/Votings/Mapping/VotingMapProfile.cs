using AutoMapper;
using TW.Training.Vote.Domain.Votings;

namespace TW.Training.Vote.Infrastructure.Votings.Mapping;

public class VotingMapProfile : Profile
{
    public VotingMapProfile()
    {
        CreateMap<Voting, PO.Voting>().ReverseMap();
    }
}