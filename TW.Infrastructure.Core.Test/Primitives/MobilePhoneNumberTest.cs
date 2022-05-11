using NUnit.Framework;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Core.Test.Primitives;

public class MobileNumberTest
{
    [Test]
    public void Test1()
    {
        var mobilePhoneNumber = new MobilePhoneNumber("15618147550");
        Assert.Pass();
    }
}