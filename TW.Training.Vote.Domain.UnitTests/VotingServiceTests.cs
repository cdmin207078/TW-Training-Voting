using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Core.Arguments;
using TW.Infrastructure.Core.Exceptions;
using TW.Infrastructure.Core.Primitives;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.Domain.Votings;

namespace TW.Training.Vote.Domain.UnitTests;

[TestClass]
public class VotingServiceTests
{
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_MobilePhoneNumber_Is_Null()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = null
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //    
    //    // ASSERT
    //    Assert.AreEqual("the user mobile-phone-number cannot be null", result.Message);
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_ProgrammeCodeNumber_Is_Null()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550")
    //     };
    //     
    //     // ACT
    //    var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //    // ASSERT
    //    Assert.AreEqual("the programme Number cannot be null", result.Message);
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_ProgrammeCodeNumber_Does_Not_Exist()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number")
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //     // ASSERT
    //     Assert.AreEqual("can't find Programme:fake-programme-code-number", result.Message);
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_VotingItems_Is_NullOrEmpty()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //     // ASSERT
    //     Assert.AreEqual("voting can not be null or empty", result.Message);
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_User_Has_Already_Complete_Voting()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     stubVotingRepository.GetVotingCount(Arg.Any<MobilePhoneNumber>(), Arg.Any<CodeNumber>()).Returns(3);
    //
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //         ProgrammeItemCodeNumbers = new List<CodeNumber>
    //         {
    //             new("fake-programme-item-code-number"),
    //             new("fake-programme-item-code-number"),
    //             new("fake-programme-item-code-number")
    //         },
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //     // ASSERT
    //     Assert.IsTrue(result.Message.Contains("you are already complete voting"));
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_User_Has_No_Remain_Voting_Count()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     stubVotingRepository.GetVotingCount(Arg.Any<MobilePhoneNumber>(), Arg.Any<CodeNumber>()).Returns(1);
    //
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //         ProgrammeItemCodeNumbers = new List<CodeNumber>
    //         {
    //             new("fake-programme-item-code-number"),
    //             new("fake-programme-item-code-number"),
    //             new("fake-programme-item-code-number")
    //         },
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //     // ASSERT
    //     Assert.IsTrue(result.Message.Contains("please check your voting items count"));
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_ThrowTWException_When_Programme_Item_CodeNumber_Not_Exist()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     stubVotingRepository.GetVotingCount(Arg.Any<MobilePhoneNumber>(), Arg.Any<CodeNumber>()).Returns(0);
    //
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //         ProgrammeItemCodeNumbers = new List<CodeNumber>
    //         {
    //             new("fake-programme-item-code-number")
    //         },
    //     };
    //     
    //     // ACT
    //     var result = await Assert.ThrowsExceptionAsync<TWException>(() => stubVotingService.Voting(fakeSubmitVotingInput));
    //
    //     // ASSERT
    //     Console.WriteLine(result.Message);
    //     Assert.IsTrue(result.Message.Contains("unknown programme item"));
    // }
    //
    // [TestMethod]
    // public async Task Voting_Should_Success_When_All_Is_Correct()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //     
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //         ProgrammeItemCodeNumbers = new List<CodeNumber>
    //         {
    //             new ("fake-programme-item-code-number-1"),
    //             new ("fake-programme-item-code-number-1"),
    //             new ("fake-programme-item-code-number-1")
    //         }
    //     };
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     stubVotingRepository.GetVotingCount(Arg.Any<MobilePhoneNumber>(), Arg.Any<CodeNumber>()).Returns(0);
    //     
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     // ACT
    //     await stubVotingService.Voting(fakeSubmitVotingInput);
    //     
    //     // ASSERT
    //     Assert.IsTrue(true);
    // }
    
    // [TestMethod]
    // public async Task Voting_Should_Success_When_All_Is_Correct_Greeting()
    // {
    //     // ARRANGE
    //     var stubProgrammeRepository = Substitute.For<IProgrammeRepository>();
    //     var fakeProgramme = Substitute.For<Programme>(GetCreateProgrammeInput(), stubProgrammeRepository);
    //     stubProgrammeRepository.Get(Arg.Any<CodeNumber>()).Returns(fakeProgramme);
    //
    //     var fakeSubmitVotingInput = new SubmitVotingInput
    //     {
    //         MobilePhoneNumber = new MobilePhoneNumber("15618147550"),
    //         ProgrammeCodeNumber = new CodeNumber("fake-programme-code-number"),
    //         ProgrammeItemCodeNumbers = new List<CodeNumber>
    //         {
    //             new ("fake-programme-item-code-number-1"),
    //             new ("fake-programme-item-code-number-1"),
    //             new ("fake-programme-item-code-number-1")
    //         }
    //     };
    //     
    //     var stubVotingRepository = Substitute.For<IVotingRepository>();
    //     stubVotingRepository.GetVotingCount(Arg.Any<MobilePhoneNumber>(), Arg.Any<CodeNumber>()).Returns(0);
    //     stubVotingRepository.GetVoting().Returns(Task.FromResult(default(Voting)));
    //     
    //     var stubVotingService = Substitute.For<VotingService>(stubVotingRepository, stubProgrammeRepository);
    //     
    //     // ACT
    //     await stubVotingService.Voting(fakeSubmitVotingInput);
    //     
    //     // ASSERT
    //     // stubVotingRepository.Received().Voting(Arg.Any<Voting>());
    //     stubVotingRepository.Received().Greeting();
    // }
    //
   
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

    private CreateProgrammeInput GetCreateProgrammeInput()
    {
        return new CreateProgrammeInput
        {
            Title = "fake-programme-title",
            PerPersonMaxVotingCount = 3,
            Code = new CodeNumber("fake-programme-code-number"),
            CreatorId = new Id<int>(1),
            Items = new List<CreateProgrammeInput.Item>
            {
                new CreateProgrammeInput.Item()
                {
                    Title = "fake-programme-item-title",
                    Code = new CodeNumber("fake-programme-item-code-number-1")
                },
            }
        };
    }
}