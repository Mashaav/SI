using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class FIFOFringe<S> : IFringe<S>
    {
        private List<S> fifo = new List<S>();

        public void Add(S element)
        {
            fifo.Add(element);
        }

        public bool Empty()
        {
            return fifo.Count > 0 ? false : true;
        }

        public S Remove()
        {
            S element = fifo.ElementAt(0);
            fifo.RemoveAt(0);
            return element;
        }
    }

    public class LIFOFringe<S> : IFringe<S>
    {
        private Stack<S> lifo = new Stack<S>();

        public void Add(S element)
        {
            lifo.Push(element);
        }

        public bool Empty()
        {
            return lifo.Count > 0 ? false : true;
        }

        public S Remove()
        {
            return lifo.Pop();
        }
    }

}
