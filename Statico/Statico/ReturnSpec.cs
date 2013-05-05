using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Statico
{
    public class ReturnSpec 
    {
        public readonly MethodBase _toReplace;
        public MethodInfo replaement;
        private Exception toThrow;

        public void Reset()
        {
            MethodUtil.ReplaceMethod(_toReplace,replaement);
        }
        public ReturnSpec(MethodBase toReplace)
        {
            _toReplace = toReplace;
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Will<T>(Func<T> action)
        {
            replaement = action.Method;
            Set();
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private void Set()
        {
            MethodUtil.ReplaceMethod(replaement,_toReplace);
            AllCalls.Behaviors.Push(this);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void Will<T>(Action action)
        {
            replaement = action.Method;
            Set();
        }


        public void ThrowIfPossible()
        {
            if (toThrow!=null)
            {
                throw toThrow;
            }
        }
        [MethodImpl(MethodImplOptions.NoOptimization)]
        public void WillThrow(Exception exception)
        {
            toThrow = exception;
            replaement = this.GetType().GetMethod("ThrowIfPossible", BindingFlags.Public | BindingFlags.Instance);
            Set();
        }

    }
}