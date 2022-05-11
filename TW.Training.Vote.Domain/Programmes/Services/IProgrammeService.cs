using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public interface IProgrammeService
{
    Task Create(CreateProgrammeInput input);
    Task Update(UpdateProgrammeInput input);
    Task Delete(DeleteProgrammeInput input);

    // Task<GetProgrammeOutput> GetProgramme(Id<int> Id);
    Task<GetProgrammeOutput> GetProgramme(CodeNumber code);
    Task<GetProgrammesOutput> GetProgrammes(GetProgrammesInput input);
}