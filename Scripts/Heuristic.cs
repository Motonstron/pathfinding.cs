using System.Collections;

namespace PathFinding {

	public enum HeuristicMode {
		Manhattan,
		Euclidean,
		Octile,
		ChebyShec
	}

	public class Heuristic {

		/// <summary>
		/// Manhattan the specified dx and dy.
		/// </summary>
		/// <param name="dx">Dx.</param>
		/// <param name="dy">Dy.</param>
		public static int Manhattan(int dx, int dy) {
			return dx + dy;
		}

		/// <summary>
		/// Euclidean the specified dx and dy.
		/// </summary>
		/// <param name="dx">Dx.</param>
		/// <param name="dy">Dy.</param>
		public static int Euclidean(int dx, int dy) {
			return (int)Mathf.Sqrt(dx * dx + dy * dy);
		}

		/// <summary>
		/// Octile the specified dx and dy.
		/// </summary>
		/// <param name="dx">Dx.</param>
		/// <param name="dy">Dy.</param>
		public static int Octile(int dx, int dy) {
			float f = Mathf.Sqrt(2) - 1;
			return (dx < dy) ? (int)f * dx + dy : (int)f * dy + dx;
		}

		/// <summary>
		/// Chebies the shev.
		/// </summary>
		/// <returns>The shev.</returns>
		/// <param name="dx">Dx.</param>
		/// <param name="dy">Dy.</param>
		public static int ChebyShev(int dx, int dy) {
			return Mathf.Max(dx, dy);
		}
	}
}