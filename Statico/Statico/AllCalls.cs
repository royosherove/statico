using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Statico
{
    public class AllCalls
    {
        public static Stack<ReturnSpec> Behaviors  = new Stack<ReturnSpec>();

        public static ReturnSpec To(Expression<Func<int>> thefunc )
        {
            var call = thefunc.Body as MethodCallExpression;
            MethodBase theMethod = call.Method;
            ReturnSpec rs = new ReturnSpec(theMethod);
            return rs;
        }

        public static void Reset()
        {
            while (Behaviors.Count>0)
            {
                var spec = Behaviors.Pop();
                spec.Reset();
            }

        }
    }
}