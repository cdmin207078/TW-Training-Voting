using NUnit.Framework;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Core.Test.Primitives;

public class CodeNumberTest
{
    [Test]
    public void Test1()
    {
        var code = new CodeNumber("China-2020-GALA");
        Assert.Pass();
    }
}