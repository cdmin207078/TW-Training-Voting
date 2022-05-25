using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace TW.Training.Vote.Doamin.UT3111111111
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        
        [Test]
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
}