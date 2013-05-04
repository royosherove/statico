using NUnit.Framework;

namespace Statico.UnitTests
{
    [TestFixture]
    public class StaticoTests
    {
        [Test]
        public void Fake_StaticWithInt_ReturnsFakeInt()
        {
            AllCalls.To(() => ClassWithStaticMethods.StaticMethodThatReturns1()).WillReturn(2);


        }
    }

    public class ClassWithStaticMethods
    {
        public int StaticMethodThatReturns1()
        {
            return 1;
        }
    }
}
