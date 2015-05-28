using System;
using System.Collections.Generic;

namespace Misc
{
    /// <summary>
    /// This is the interface for a Indexed Priority Queue
    /// </summary>
    /// <remarks>
    /// Based on BenDi's Priority Queue code at CodeProject.
    /// (http://www.codeproject.com/csharp/priorityqueue.asp)
    /// </remarks>
    internal interface IPriorityQueue<T> : ICollection<T>, IList<T>
    {
        int Push(T element);

        T Pop();

        T Peek();

        void Update(int i);
    }
}