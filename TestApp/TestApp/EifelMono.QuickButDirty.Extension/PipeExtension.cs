using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace EifelMono.QuickButDirty.Extension {
    public static class PipeExtension
    {
        public static T Pipe<T>(this T pipe, Action<T> action)
        {
            action(pipe);
            return pipe;
        }

        public static T Pipe<T>(this T pipe, Func<T, T> action) => action(pipe);
    }
}
