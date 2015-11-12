using System;
using System.Collections.Generic;

namespace Saturn72.Extensions
{
    public static class ActionExtensions
    {
        public static void Run(this Action action)
        {
            action();
        }

        public static void RunAll(this IEnumerable<Action> actions)
        {
            actions.ForEachItem(act => act());
        }
    }
}