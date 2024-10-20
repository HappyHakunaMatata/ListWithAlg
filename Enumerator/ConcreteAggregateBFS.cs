using System;
namespace Enumerator
{
	public class ConcreteAggregateBFS<T> : IEnumerable<T>
    {

        private ListWithAlgorithm<T> _Container;

        public ConcreteAggregateBFS(ListWithAlgorithm<T> list)
        {
            _Container = list;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ConcreteEnumerator<T> GetEnumerator()
        {
            return new ConcreteEnumerator<T>(_Container);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}

