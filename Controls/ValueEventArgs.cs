using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Controls
{
    public class ValueEventArgs<T> : EventArgs
    {
        public readonly T Argument;

        public ValueEventArgs(T arg)
        {
            Argument = arg;
        }
    }
}
