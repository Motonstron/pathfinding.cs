using System.Collections.Generic;
using System.Collections;

namespace PathFinding {
	public class NodeComparer : Comparer<INode> {
		
		/// <summary>
		/// Compare the specified x and y.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public override int Compare (INode x, INode y)
		{
			return (int)x.Utils.f - (int)y.Utils.f;
		}
	}
}