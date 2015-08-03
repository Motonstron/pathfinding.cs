using System;
using System.Collections.Generic;

namespace Pathfinding.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeapQ<T>
    {
        private readonly List<T> _heap;

        protected IComparer<T> Comparer { get; }

        public HeapQ(IComparer<T> comparer)
        {
            _heap = new List<T>();

            if (comparer == null)
                throw new Exception("Comparer is null");

            Comparer = comparer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            _heap.Add(item);
            SiftDown(0, _heap.Count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T item;
            var last = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            if (_heap.Count > 0)
            {
                item = _heap[0];
                _heap[0] = last;
                SiftUp(0);
            }
            else
            {
                item = last;
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(T item)
        {
            var pos = _heap.IndexOf(item);
            if (pos == -1)
            {
                return;
            }

            SiftDown(0, pos);
            SiftUp(pos);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get { return _heap.Count == 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get { return _heap.Count - 1; }
        }

        private void SiftDown(int startPos, int pos)
        {
            var newitem = _heap[pos];
            while (pos > startPos)
            {
                var parentpos = (pos - 1) >> 1;
                var parent = _heap[parentpos];
                if (Comparer.Compare(newitem, parent) < 0)
                {
                    _heap[pos] = parent;
                    pos = parentpos;
                    continue;
                }
                break;
            }
            _heap[pos] = newitem;
        }

        private void SiftUp(int pos)
        {
            var endpos = _heap.Count;
            var startpos = pos;
            var newitem = _heap[pos];
            var childpos = 2 * pos + 1;
            while (childpos < endpos)
            {
                var rightpos = childpos + 1;
                if (rightpos < endpos && !(Comparer.Compare(_heap[childpos], _heap[rightpos]) < 0))
                {
                    childpos = rightpos;
                }
                _heap[pos] = _heap[childpos];
                pos = childpos;
                childpos = 2 * pos + 1;
            }
            _heap[pos] = newitem;
            SiftDown(startpos, pos);
        }
    }
}