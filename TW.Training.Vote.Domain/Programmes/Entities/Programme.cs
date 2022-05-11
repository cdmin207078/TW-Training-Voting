using System.Runtime.CompilerServices;
using TW.Infrastructure.Domain.Entities.Auditing;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class Programme : FullAuditedEntity<int>
{
    #region Constructors

    protected Programme()
    {
    }

    public Programme(CreateProgrammeInput input, IProgrammeRepository programmeRepository)
    {
        if (input is null)
            throw new ArgumentException(nameof(input));
        if (programmeRepository is null)
            throw new ArgumentException(nameof(programmeRepository));

        SetCreation(input.CreatorId);
        SetTitle(input.Title);
        SetPerPersonVotingCount(input.PerPersonMaxVotingCount);
        SetProgrammeItems(input.Items);
        SetCode(input.Code, programmeRepository).GetAwaiter().GetResult();

        Description = input.Description.Trim();
    }

    #endregion

    #region Propertities

    public CodeNumber Code { get; set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public int PersonalMaxVotingCount { get; protected set; }

    private readonly List<ProgrammeItem> _programmeItems = new();
    public IReadOnlyCollection<ProgrammeItem> ProgrammeItems => _programmeItems;

    #endregion

    #region Methods
    
    public async Task Update(UpdateProgrammeInput input, IProgrammeRepository programmeRepository)
    {
        if (input is null)
            throw new ArgumentException(nameof(input));
        if (programmeRepository is null)
            throw new ArgumentException(nameof(programmeRepository));

        SetLastModified(input.LastModifierId);
        SetTitle(input.Title);
        SetPerPersonVotingCount(input.PerPersonMaxVotingCount);
        SetProgrammeItems(input.Items);
        await SetCode(input.Code, programmeRepository);

        Description = input.Description.Trim();
    }

    private async Task SetCode(CodeNumber code, IProgrammeRepository programmeRepository)
    {
        if (code is null)
            throw new ArgumentException("code can not be null or empty");

        // if unchanged
        if (code.Equals(Code))
            return;

        var exists = await programmeRepository.IsExists(code);
        if (exists)
            throw new ArgumentException("code is exists");

        Code = code;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("title can not be null or empty");

        Title = title.Trim();
    }

    private void SetPerPersonVotingCount(int count)
    {
        if (count < 1)
            throw new ArgumentException("PerPersonVotingCount can't less than zero");

        PersonalMaxVotingCount = count;
    }

    private void SetProgrammeItems(List<CreateProgrammeInput.Item> items)
    {
        if (items is null || !items.Any())
            return;

        // check duplicatedCodeNumber
        var duplicatedCodeNumber = items.GroupBy(x => x.Code).Where(x => x.Count() > 1).FirstOrDefault()?.Key;
        if (duplicatedCodeNumber is not null)
            throw new ArgumentException($"duplicate item code:{duplicatedCodeNumber}");

        foreach (var item in items)
        {
            var programmeItem = new ProgrammeItem(item, CreatorId);
            _programmeItems.Add(programmeItem);
        }
    }

    private void SetProgrammeItems(List<UpdateProgrammeInput.Item> items)
    {
        if (items is null || !items.Any())
        {
            _programmeItems.Clear();
            return;
        }
        
        // check duplicatedCodeNumber
        var duplicatedCodeNumber = items.GroupBy(x => x.Code).Where(x => x.Count() > 1).FirstOrDefault()?.Key;
        if (duplicatedCodeNumber is not null)
            throw new ArgumentException($"duplicate item code:{duplicatedCodeNumber}");
        
        // removing
        _programmeItems.RemoveAll(x => !items.Any(z => z.Id.Equals(x.Id)));

        // update or add
        foreach (var item in items)
        {
            var programmeItem = _programmeItems.FirstOrDefault(x => x.Id.Equals(item.Id));
            if (programmeItem is null)
            {
                var data = new CreateProgrammeInput.Item
                {
                    Code = item.Code,
                    Title = item.Title,
                    Order = item.Order,
                    Description = item.Description
                };
                _programmeItems.Add(new ProgrammeItem(data, LastModifierId));
            }
            else
            {
                programmeItem.Update(item, LastModifierId);
            }
        }
    }

    #endregion
}