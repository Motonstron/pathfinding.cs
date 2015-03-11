using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding {
	
	public class HeapQ<T> {

		private List<T> _heap;

		protected Comparer<T> Comparer { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeapQ`1"/> class.
		/// </summary>
		/// <param name="comparer">Comparer.</param>
		public HeapQ(Comparer<T> comparer)
		{
			_heap = new List<T>();

			if(comparer == null)
				throw new ArgumentNullException("Comparer is null");

			Comparer = comparer;
		}

		/// <summary>
		/// Push the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Push(T item) {
			_heap.Add(item);
			SiftDown(0, _heap.Count - 1);
		}

		/// <summary>
		/// Pop this instance.
		/// </summary>
		public T Pop() {
			T item;
			var last = _heap[_heap.Count - 1];
			_heap.RemoveAt (_heap.Count - 1);
			if (_heap.Count > 0) {
				item = _heap[0];
				_heap[0] = last;
				SiftUp (0);
			} else {
				item = last;
			}
			return item;
		}

		/// <summary>
		/// Updates the item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void UpdateItem(T item) {
			var pos = _heap.IndexOf(item);
			if(pos == -1) {
				return;
			}

			SiftDown(0, pos);
			SiftUp(pos);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="HeapQ`1"/> is empty.
		/// </summary>
		/// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
		public bool Empty {
			get {
				return _heap.Count == 0;
			}
		}

		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <value>The size.</value>
		public int Size {
			get {
				return _heap.Count - 1;
			}
		}

		/// <summary>
		/// Sifts down.
		/// </summary>
		/// <param name="startpos">Startpos.</param>
		/// <param name="pos">Position.</param>
		private void SiftDown (int startpos, int pos)
		{
			var newitem = _heap[pos];
			while (pos > startpos) {
				var parentpos = (pos - 1) >> 1;
				var parent = _heap[parentpos];
				if (Comparer.Compare(newitem, parent) < 0) {
					_heap[pos] = parent;
					pos = parentpos;
					continue;
				}
				break;
			}
			_heap[pos] = newitem;
		}

		/// <summary>
		/// Sifts up.
		/// </summary>
		/// <param name="pos">Position.</param>
		private void SiftUp (int pos)
		{
			var endpos = _heap.Count;
			var startpos = pos;
			var newitem = _heap[pos];
			var childpos = 2 * pos + 1;
			while (childpos < endpos) {
				var rightpos = childpos + 1;
				if (rightpos < endpos && !(Comparer.Compare(_heap[childpos], _heap[rightpos]) < 0)) {
					childpos = rightpos;
				}
				_heap[pos] = _heap[childpos];
				pos = childpos;
				childpos = 2 * pos + 1;
			}
			_heap[pos] = newitem;
			SiftDown (startpos, pos);
		}
	}
}