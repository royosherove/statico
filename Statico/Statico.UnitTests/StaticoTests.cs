using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using NativeAssemblerInjection;

namespace Statico.UnitTests
{


    class Person
    {
        public int Return1()
        {
            return 1;
        } 
    }

    [TestFixture]
    public class StaticoTests
    {
        private Person p = new Person();
        private Person p2 = new Person();

        [Test]
        public void Fake_StaticWithInt_ReturnsFakeInt()
        {
            AllCalls.To(() => ClassWithStaticMethods.StaticMethodThatReturns1()).Will(()=> 2);

            Assert.AreEqual(2, ClassWithStaticMethods.StaticMethodThatReturns1());
        }
        [Test]
        public void Fake_NonStatic_WorksOnFutures()
        {
            AllCalls.To(() => new Person().Return1()).Will(() => 2);

            Assert.AreEqual(2, new Person().Return1());
            Assert.AreEqual(2, p.Return1());
            Assert.AreEqual(2, p2.Return1());
        }
    }

    public class AllCalls
    {
        public static ReturnSpec To(Expression<Func<int>> thefunc )
        {
            var call = thefunc.Body as MethodCallExpression;
            MethodBase theMethod = call.Method;
            ReturnSpec rs = new ReturnSpec(theMethod);
            return rs;
        }
    }

    public class ReturnSpec
    {
        public readonly MethodBase _toReplace;
        public MethodInfo replaement;

        public void Dispose()
        {
        }
        public ReturnSpec(MethodBase toReplace)
        {
            _toReplace = toReplace;
        }

        public void Will<T>(Func<T> action)
        {
            replaement = action.Method;
            MethodUtil.ReplaceMethod(replaement,_toReplace);
        }

        public void Will<T>(Action<T> action)
        {
            replaement = action.Method;
            MethodUtil.ReplaceMethod(replaement,_toReplace);
        }
    }

    public class ClassWithStaticMethods
    {
        public static int StaticMethodThatReturns2()
        {
            return 2;
        }
        public static int StaticMethodThatReturns1()
        {
            return 1;
        }
    }
}
