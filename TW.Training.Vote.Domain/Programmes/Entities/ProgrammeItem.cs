using TW.Infrastructure.Core.Exceptions;
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
    //         throw new TWException(nameof(input));
    //     if (programmeRepository == null)
    //         throw new TWException(nameof(programmeRepository));
    //
    //     SetCreation(input.CreatorId);
    //     SetOrder(input.Order);
    //     SetTitle(input.Title);
    //     SetCode(input.code, programmeRepository).GetAwaiter().GetResult();
    //     
    //     Description = input.Description.Trim();
    // }

    public ProgrammeItem(CreateProgrammeInput.Item input, Programme programme, Id<int> creatorId)
    {
        if (input == null) 
            throw new TWException(nameof(input));
        
        SetCreation(creatorId);
        SetOrder(input.Order);
        SetTitle(input.Title);
        SetCode(input.Code);
        
        Description = input.Description?.Trim();
        Programme = programme;
    }

    #endregion

    #region Propertities

    public int Order { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    
    public CodeNumber Code { get; protected set; }
    public Programme Programme { get; protected set; }
    
    #endregion
    
    #region Methods

    // public async Task Update(CreateProgrammeItemInput input, IProgrammeRepository programmeRepository)
    // {
    //     if (input == null) 
    //         throw new TWException(nameof(input));
    //     if (programmeRepository == null)
    //         throw new TWException(nameof(programmeRepository)); 
    //     
    //     SetLastModified(input.LastModifierId);
    //     SetTitle(input.Title);
    //     SetOrder(input.Order);
    //     await SetCode(input.code, programmeRepository);
    //     
    //     Description = input.Description.Trim();
    // }

    public void Update(UpdateProgrammeInput.Item input, Id<int> lastModifierId)
    {
        if (input == null)
            throw new TWException(nameof(input));
        
        SetLastModified(lastModifierId);
        SetTitle(input.Title);
        SetOrder(input.Order); 
        SetCode(input.Code);
        
        Description = input.Description?.Trim();
    }

    private void SetOrder(int order)
    {
        if (order < 0)
            throw new TWException("order can can't less than zero");
        
        Order = order;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new TWException("title can not be null or empty");

        Title = title.Trim();
    }

    private void SetCode(CodeNumber code)
    {
        if (code is null)
            throw new TWException("code can not be null or empty");

        Code = code;
    }

    private async Task SetCode(CodeNumber code, IProgrammeRepository programmeRepository)
    {
        if(code is null)
            throw new TWException("code can not be null or empty");
        
        // if unchanged
        if (code.Equals(Code)) 
            return;

        var exists = await programmeRepository.IsExists(code);
        if (exists)
            throw new TWException("code is exists");

        Code = code;
    }

    #endregion
}