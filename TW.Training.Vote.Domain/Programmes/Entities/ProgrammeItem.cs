using TW.Infrastructure.Domain.Entities.Auditing;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class ProgrammeItem: FullAuditedEntity<int>
{
    #region Constructors

    protected ProgrammeItem()
    {
    }

    // public ProgrammeItem(CreateProgrammeItemInput input, IProgrammeRepository programmeRepository)
    // {
    //     if (input == null) 
    //         throw new ArgumentException(nameof(input));
    //     if (programmeRepository == null)
    //         throw new ArgumentException(nameof(programmeRepository));
    //
    //     SetCreation(input.CreatorId);
    //     SetOrder(input.Order);
    //     SetTitle(input.Title);
    //     SetCode(input.Code, programmeRepository).GetAwaiter().GetResult();
    //     
    //     Description = input.Description.Trim();
    // }

    public ProgrammeItem(CreateProgrammeInput.Item input, Id<int> creatorId)
    {
        if (input == null) 
            throw new ArgumentNullException(nameof(input));
        
        SetCreation(creatorId);
        SetOrder(input.Order);
        SetTitle(input.Title);
        SetCode(input.Code);
        
        Description = input.Description.Trim();
    }

    #endregion

    #region Propertities

    public int Order { get; protected set; }
    public CodeNumber Code { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    
    #endregion
    
    #region Methods

    // public async Task Update(CreateProgrammeItemInput input, IProgrammeRepository programmeRepository)
    // {
    //     if (input == null) 
    //         throw new ArgumentException(nameof(input));
    //     if (programmeRepository == null)
    //         throw new ArgumentException(nameof(programmeRepository)); 
    //     
    //     SetLastModified(input.LastModifierId);
    //     SetTitle(input.Title);
    //     SetOrder(input.Order);
    //     await SetCode(input.Code, programmeRepository);
    //     
    //     Description = input.Description.Trim();
    // }

    public void Update(UpdateProgrammeInput.Item input, Id<int> lastModifierId)
    {
        if (input == null)
            throw new ArgumentException(nameof(input));
        
        SetLastModified(lastModifierId);
        SetTitle(input.Title);
        SetOrder(input.Order); 
        SetCode(input.Code);
        
        Description = input.Description.Trim();
    }

    private void SetOrder(int order)
    {
        if (order < 0)
            throw new ArgumentException("order can can't less than zero");
        
        Order = order;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("title can not be null or empty");

        Title = title.Trim();
    }

    private void SetCode(CodeNumber code)
    {
        if (code is null)
            throw new ArgumentException("code can not be null or empty");

        Code = code;
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

    #endregion
}