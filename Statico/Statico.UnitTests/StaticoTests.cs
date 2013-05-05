using System;
using NUnit.Framework;

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

        [TearDown]
        public void teardown()
        {
            AllCalls.Reset();
        }
        [Test]
        public void Fake_StaticWithInt_ReturnsFakeInt()
        {
            AllCalls.To(() => ClassWithStaticMethods.StaticMethodThatReturns1()).Will(()=> 2);

            Assert.AreEqual(2, ClassWithStaticMethods.StaticMethodThatReturns1());
        }
        
        [Test]
        public void Fake_StaticWithInt_CanThrow()
        {
            AllCalls.To(() => ClassWithStaticMethods.StaticMethodThatReturns1()).WillThrow(new Exception("fake"));

            Assert.Throws<Exception>(()=>
                ClassWithStaticMethods.StaticMethodThatReturns1() );
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
