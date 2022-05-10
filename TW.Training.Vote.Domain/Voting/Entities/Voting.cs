using TW.Infrastructure.Domain.Entities;
using TW.Infrastructure.Domain.Entities.Auditing;
using TW.Infrastructure.Domain.Primitives;
using TW.Training.Vote.Domain.Programmes;

namespace TW.Training.Vote.Domain.Voting;

public class Voting
{
    #region Contructors

    public Voting(CreateVotingInput input, IProgrammeRepository programmeRepository, IVotingRepository votingRepository)
    {
        if (input == null) 
            throw new ArgumentNullException(nameof(input));
        if (programmeRepository == null) 
            throw new ArgumentNullException(nameof(programmeRepository));
        if (votingRepository == null) 
            throw new ArgumentNullException(nameof(votingRepository));

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
    public List<ProgrammeItem> VotesProgrammeItems { get; protected set; }

    #endregion
    
    #region Methods

    private void SetMobilePhoneNumber(MobilePhoneNumber mobilePhoneNumber)
    {
        if (mobilePhoneNumber is null) 
            throw new ArgumentNullException(nameof(mobilePhoneNumber));
        
        MobilePhoneNumber = mobilePhoneNumber;
    }
    
    private async Task SetProgramme(CodeNumber codeNumber,IProgrammeRepository programmeRepository)
    {
        if (codeNumber is null)
            throw new ArgumentNullException(nameof(codeNumber));

        var programme = await programmeRepository.Get(codeNumber);
        if (programme is null)
            throw new ArgumentException($"can't find Programme:{codeNumber}");

        Programme = programme;
    }
    
    private async Task SetProgrammeItems(List<CodeNumber> programmeItems, IVotingRepository votingRepository)
    {
        if (programmeItems is null || !programmeItems.Any())
            throw new ArgumentNullException($"voting can not be null or empty");
        
        // validate voting count
        var existVotingCount = await votingRepository.GetVotingCount(MobilePhoneNumber, Programme.Id);
        var remainVotingCount = Programme.PersonalMaxVotingCount - existVotingCount;
        if (remainVotingCount == 0)
            throw new ArgumentException($"Dear: {MobilePhoneNumber}, you are already complete voting for {Programme.Title}. can not repeat voting");
        if (remainVotingCount != programmeItems.Count)
            throw new ArgumentException($"Dear: {MobilePhoneNumber} remaining have {remainVotingCount} left to voting. please check your voting items count");

        var expects = programmeItems.Except(Programme.ProgrammeItems.Select(x => x.Code).ToList());
        if (expects.Any())
            throw new ArgumentException($"unknown programme item: {string.Join(',', expects)}");

        VotesProgrammeItems = Programme.ProgrammeItems.Where(x => programmeItems.Any(c => c.Equals(x.Code))).ToList();
    }
    
    #endregion
}