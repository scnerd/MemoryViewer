using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryViewer
{
    public class IteratorList<T> : System.Collections.Generic.IList<T>
    {
        private List<T> buffered = new List<T>();
        private IEnumerator<T> iterator;
        private int ct = -1;

        public IteratorList(IEnumerator<T> Iterator)
        {
            this.iterator = Iterator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return iterator;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return iterator;
        }

        public int Count
        {
            get { return ct; }
        }

        public int IndexOf(T item)
        {
            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public void RemoveAt(int index)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public T this[int index]
        {
            get
            {
                while (buffered.Count <= index && ct != -1)
                {
                    buffered.Add(iterator.Current);
                    if (!iterator.MoveNext())
                    {
                        ct = buffered.Count;
                    }
                }
                if (index >= buffered.Count)
                    throw new IndexOutOfRangeException();
                return buffered[index];
            }
            set
            {
                throw new InvalidOperationException("IteratorList is read-only");
            }
        }

        public void Add(T item)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public void Clear()
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public bool Contains(T item)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            throw new InvalidOperationException("IteratorList is read-only");
        }
    }
}
