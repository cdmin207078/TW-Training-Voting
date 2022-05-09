using TW.Infrastructure.Domain.Entities.Auditing;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class Programme: FullAuditedEntity<int>
{
    #region Constructors

    protected Programme()
    {
    }

    public Programme(CreateProgrammeInput input, IProgrammeRepository programmeRepository)
    {
        if (input == null) 
            throw new ArgumentException(nameof(input));
        if (programmeRepository == null)
            throw new ArgumentException(nameof(programmeRepository));

        SetTitle(input.Title);
        SetPerPersonVotingCount(input.PerPersonMaxVotingCount);
        SetCode(input.Code, programmeRepository).GetAwaiter().GetResult();
        
        Description = input.Description.Trim();
    }

    #endregion

    #region Propertities

    public CodeNumber Code { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public int PerPersonMaxVotingCount { get; protected set; }

    private readonly List<ProgrammeItem> _items = new();
    public IReadOnlyCollection<ProgrammeItem> Items => _items;
    
    #endregion
    
    #region Methods

    public async Task Update(UpdateProgrammeInput input, IProgrammeRepository programmeRepository)
    {
        if (input == null) 
            throw new ArgumentException(nameof(input));
        if (programmeRepository == null)
            throw new ArgumentException(nameof(programmeRepository)); 
        
        SetTitle(input.Title);
        SetPerPersonVotingCount(input.PerPersonMaxVotingCount);
        await SetCode(input.Code, programmeRepository);
        
        Description = input.Description.Trim();
    }
    
    private void SetPerPersonVotingCount(int count)
    {
        if (count < 1)
            throw new ArgumentException("PerPersonVotingCount can't less than zero");

        PerPersonMaxVotingCount = count;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("title can not be null or empty");

        Title = title.Trim();
    }

    private async Task SetCode(CodeNumber code, IProgrammeRepository programmeRepository)
    {
        if(code is null)
            throw new ArgumentException("code can not be null or empty");
        
        // if unchanged
        if (code.Equals(Code)) 
            return;

        var exists = await programmeRepository.IsExists(code);
        if (exists)
            throw new ArgumentException("code is exists");

        Code = code;
    }
    
    private async Task SetItems(IProgrammeRepository programmeRepository)
    {
    }

    #endregion
    
    #region AssociatedEntity
    
    public class ProgrammeItem
    {
        public string Code { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
    }

    #endregion
}