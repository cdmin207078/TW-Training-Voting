using TW.Infrastructure.Core.Exceptions;
using TW.Infrastructure.Core.Primitives;
using TW.Training.Vote.Domain.Programmes;

namespace TW.Training.Vote.Domain.Votings;

public class Voting
{
    #region Contructors

    public Voting(SubmitVotingInput input, IProgrammeRepository programmeRepository, IVotingRepository votingRepository)
    {
        if (input == null) 
            throw new TWException(nameof(input));
        if (programmeRepository == null) 
            throw new TWException(nameof(programmeRepository));
        if (votingRepository == null) 
            throw new TWException(nameof(votingRepository));

        SetMobilePhoneNumber(input.MobilePhoneNumber);
        SetProgramme(input.ProgrammeCodeNumber, programmeRepository).GetAwaiter().GetResult();
        SetProgrammeItems(input.ProgrammeItemCodeNumbers, votingRepository).GetAwaiter().GetResult();
        
        Name = string.IsNullOrWhiteSpace(input.Name) ? null : input.Name.Trim();
    }

    #endregion
    
    #region Propertities
    
    private Programme Programme { get; set; }

    public string Name { get; protected set; }
    public MobilePhoneNumber MobilePhoneNumber { get; protected set; }
    public List<ProgrammeItem> VoteItems { get; protected set; }

    #endregion
    
    #region Methods

    private void SetMobilePhoneNumber(MobilePhoneNumber mobilePhoneNumber)
    {
        if (mobilePhoneNumber is null) 
            throw new TWException("the user mobile-phone-number cannot be null");
        
        MobilePhoneNumber = mobilePhoneNumber;
    }
    
    private async Task SetProgramme(CodeNumber codeNumber,IProgrammeRepository programmeRepository)
    {
        if (codeNumber is null)
            throw new TWException("the programme Number cannot be null");

        var programme = await programmeRepository.Get(codeNumber);
        if (programme is null)
            throw new TWException($"can't find Programme:{codeNumber}");

        Programme = programme;
    }

    private async Task SetProgrammeItems(List<CodeNumber> programmeItems, IVotingRepository votingRepository)
    {
        if (programmeItems is null || !programmeItems.Any())
            throw new TWException($"voting can not be null or empty");

        // validate voting count
        var existVotingCount = await votingRepository.GetVotingCount(MobilePhoneNumber, Programme.Code);
        var remainVotingCount = Programme.PersonalMaxVotingCount - existVotingCount;
        if (remainVotingCount < 1)
            throw new TWException(
                $"Dear: {MobilePhoneNumber}, you are already complete voting for {Programme.Title}. can not repeat voting");
        if (remainVotingCount < programmeItems.Count)
            throw new TWException(
                $"Dear: {MobilePhoneNumber} remaining have {remainVotingCount} left to voting. please check your voting items count");

        // validate vote exists
        var expects = programmeItems.Except(Programme.ProgrammeItems.Select(x => x.Code).ToList());
        if (expects.Any()) 
            throw new TWException($"unknown programme item: {string.Join(',', expects)}");
        
        // set votes
        var voteItems = new List<ProgrammeItem>();
        foreach (var item in programmeItems)
        {
            var vote = Programme.ProgrammeItems.FirstOrDefault(x => x.Code == item);
            if (vote is not null)
                voteItems.Add(vote);
        }
        VoteItems = voteItems;
    }

    #endregion
}