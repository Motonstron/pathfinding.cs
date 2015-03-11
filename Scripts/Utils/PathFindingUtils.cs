using System.Collections;
using System.Collections.Generic;

namespace PathFinding {
	public static class PathFindingUtils {

		public static List<INode> Backtrace(INode node) {

			var path = new List<INode>();
			path.Add(node);

			while(node.Utils.parent != null) {
				node = node.Utils.parent;
				path.Add(node);
			}

			path.Reverse();
			return path;
		}

		public static void BiBacktrace(Node nodeA, Node nodeB) {

		}

		public static void PathLength(List<INode> path) {

		} 

		public static void Interpolate(int x0, int y0, int x1, int y1) {

		}

		public static void ExpandPath(List<INode> path) {

		}

		public static void SmoothenPath(IGrid grid, List<INode> path) {

		}

		public static void CompressPath(List<INode> path) {

		}
	}
}