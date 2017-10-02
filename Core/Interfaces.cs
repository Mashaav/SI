using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IFringe<T>
    {
        void Add(T element);
        bool Empty();
        T Remove();
    }

    public interface IProblem<S>
    {
        S InitialState { get; }
        bool IsGoal(S state);
        IList<S> Expand(S state);
    }

}
