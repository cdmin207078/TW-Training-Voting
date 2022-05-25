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
    
    [Test]
    public void TestTest()
    {
        CodeNumber c0 = null;
        var c1 = (AbstractPrimitiveObject<string>)c0;
        var c2 = c0 as AbstractPrimitiveObject<string>;
        
        Assert.AreEqual(null, c1);
        Assert.AreEqual(null, c2);

    }
}