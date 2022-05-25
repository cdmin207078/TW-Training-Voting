using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace TW.Training.Vote.Doamin.UT31
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void adfadf()
        {
            Console.WriteLine("asdas");
        }

        [TestMethod]
        public async Task Voting_SubmitVotingModel_Test()
        {
            // ARRANGE
            var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
            var stubVotingRepository = Substitute.For<IVotingRepository>();
            stubVotingRepository.GetSubmitVotingModel(Arg.Any<string>()).Returns(default(SubmitVotingModel));
        
            var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
        
            // ACT
            await stubVotingService.Submit();
        
            // ASSERT
            // stubVotingRepository.Received().Voting(default(Voting));
            stubVotingRepository.Received().Greeting();
        }
    }
}

public class Voting
{
    
}

public class VotingService
{
   public Task Submit()
    {
        throw new NotImplementedException();
    }
}

public interface IVotingRepository
{
    Task Voting(Voting voting);

    Task<Voting> GetVoting();
    Task Greeting();

    ISubmitVotingModel GetSubmitVotingModel(string name);
}

public interface IProgrammeRepository
{
    
}

public interface ISubmitVotingModel
{
    
}

public class SubmitVotingModel : ISubmitVotingModel
{
    
}