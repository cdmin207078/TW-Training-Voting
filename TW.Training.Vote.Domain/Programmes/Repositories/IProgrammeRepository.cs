using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public interface IProgrammeRepository
{
    Task Create(Programme programme);
    Task Update(Programme programme);
    Task Delete(DeleteProgrammeInput input);

    Task<bool> IsExists(Id<int> id);
    Task<bool> IsExists(CodeNumber codeNumber);
    
    Task<Programme> Get(Id<int> id);
    Task<Programme> Get(CodeNumber codeNumber);
    Task<GetProgrammesOutput> Get(GetProgrammesInput input);
}