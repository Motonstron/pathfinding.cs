using System.Collections;

namespace PathFinding {
	public enum SearchState {
		Undefined,
		Opened,
		Closed
	}

	public class NodeUtils {

		/// <summary>
		/// The f.
		/// </summary>
		public float f;

		/// <summary>
		/// The g.
		/// </summary>
		public float g;

		/// <summary>
		/// The h.
		/// </summary>
		public float h;

		/// <summary>
		/// The parent.
		/// </summary>
		public INode parent;

		/// <summary>
		/// The state of the search.
		/// </summary>
		public SearchState searchState = SearchState.Undefined;

		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset() {
			f = g = h = 0;
			parent = null;
			searchState = SearchState.Undefined;
		}
	}
}