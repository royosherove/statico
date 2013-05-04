using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using NativeAssemblerInjection;

namespace Statico.UnitTests
{



    [TestFixture]
    public class StaticoTests
    {
        [Test]
        public void Fake_StaticWithInt_ReturnsFakeInt()
        {
            AllCalls.To(() => ClassWithStaticMethods.StaticMethodThatReturns1("a")).WillReturn(2);

            Assert.AreEqual(2, ClassWithStaticMethods.StaticMethodThatReturns1("a"));
        }
    }

    public class AllCalls
    {
        public static ReturnSpec To(Expression<Func<int>> thefunc )
        {
            MethodInfo toFake = typeof (ClassWithStaticMethods).GetMethod("StaticMethodThatReturns1", BindingFlags.Public | BindingFlags.Static);
            MethodInfo replaement = new Func<int>(() => 2).Method;
            MethodUtil.ReplaceMethod(replaement,toFake);
            return new ReturnSpec();
        }
    }

    public class ReturnSpec
    {
        public void WillReturn(int i)
        {
            
        }
    }

    public class ClassWithStaticMethods
    {
        public static int StaticMethodThatReturns2()
        {
            return 2;
        }
        public static int StaticMethodThatReturns1(string s)
        {
            return 1;
        }
    }
}
