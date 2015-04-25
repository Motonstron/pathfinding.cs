using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding {
	public static class PathFindingUtils {

		/**
		 * Backtrace according to the parent records and return the path.
		 * (including both start and end nodes)
		 */
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

		/**
		 * Backtrace from start and end node, and return the path.
		 * (including both start and end nodes)
		 */
		public static List<INode> BiBacktrace(Node nodeA, Node nodeB) 
		{
			var pathA = Backtrace(nodeA);
			var pathB = Backtrace(nodeB);

			pathB.Reverse();
			return pathA.Concat(pathB).ToList();
		}

		public static int PathLength(List<INode> path) 
		{
			var sum = 0;

			for(var i = 1; i < path.Count; ++i)
			{
				var a = path[i - 1];
				var b = path[i];
				var dx = a.PosX - b.PosX;
				var dy = a.PosY - b.PosY;

				sum += (int)Math.Sqrt(dx * dx + dy * dy);
			}
			return sum;
		} 

		/**
		 * Given the start and end coordinates, return all the coordinates lying
		 * on the line formed by these coordinates, based on Bresenham's algorithm.
		 * http://en.wikipedia.org/wiki/Bresenham's_line_algorithm#Simplification
		 */
		public static List<INode> Interpolate(int x0, int y0, int x1, int y1) 
		{
			var line = new List<INode>();

			var dx = Math.Abs(x1 - x0);
			var dy = Math.Abs(y1 - y0);

			var sx = (x0 < x1) ? 1 : -1;
			var sy = (y0 < y1) ? 1 : -1;

			var err = dx - dy;

			while(true)
			{
				var node = new Node(x0, y0);
				line.Add(node);
				
				if(x0 == x1 && y0 == y1)
					break;

				var e2 = 2 * err;
				if(e2 > -dy)
				{
					err = err - dy;
					x0 = x0 + sx;
				}

				if(e2 < dx)
				{
					err = err + dx;
					y0 = y0 + sy;
				}
			}

			return line;
		}

		/**
		 * Given a compressed path, return a new path that has all the segments
		 * in it interpolated.
		 */
		public static void ExpandPath(List<INode> path) {

		}

		/**
		 * Smoothen the give path.
		 * The original path will not be modified; a new path will be returned.
		 */
		public static void SmoothenPath(IGrid grid, List<INode> path) {

		}

		/**
		 * Compress a path, remove redundant nodes without altering the shape
		 * The original path is not modified
		 */
		public static void CompressPath(List<INode> path) {

		}
	}
}