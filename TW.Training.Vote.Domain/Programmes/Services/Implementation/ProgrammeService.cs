using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Core.Exceptions;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class ProgrammeService: IProgrammeService
{
    private readonly IObjectMapperComponent _mapper;
    private readonly IProgrammeRepository _programmeRepository;

    public ProgrammeService(IObjectMapperComponent mapper, IProgrammeRepository programmeRepository)
    {
        _mapper = mapper;
        _programmeRepository = programmeRepository;
    }
    
    public async Task Create(CreateProgrammeInput input)
    {
        var programme = new Programme(input, _programmeRepository);
        await _programmeRepository.Create(programme);
    }

    public async Task Update(UpdateProgrammeInput input)
    {
        if (input == null) 
            throw new TWException(nameof(input));

        var programme = await _programmeRepository.Get(input.Id);
        if (programme is null)
            throw new TWException($"programme: {input.Id} not exist");

        await programme.Update(input, _programmeRepository);
        await _programmeRepository.Update(programme);
    }

    public async Task Delete(DeleteProgrammeInput input)
    {
        if (input?.Id is null)
            throw new TWException(nameof(input));

        var exists = await _programmeRepository.IsExists(input.Id);
        if(!exists)
            throw new TWException($"programme: {input.Id} not exist");

        await _programmeRepository.Delete(input);
    }
    
    public async Task<GetProgrammeOutput> GetProgramme(Id<int> id)
    {
        var programme = await _programmeRepository.Get(id);
        return _mapper.Map<GetProgrammeOutput>(programme);
    }

    public async Task<GetProgrammeOutput> GetProgramme(CodeNumber codeNumber)
    {
        var programme = await _programmeRepository.Get(codeNumber);
        return _mapper.Map<GetProgrammeOutput>(programme);
    }
    
    public async Task<GetProgrammesOutput> GetProgrammes(GetProgrammesInput input)
    {
        return await _programmeRepository.Get(input);
    }
}