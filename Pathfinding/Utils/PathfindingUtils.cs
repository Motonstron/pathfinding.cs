using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class PathfindingUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<INode> Backtrace(INode node)
        {
            var path = new List<INode> {node};

            while (node.Utils.Parent != null)
            {
                node = node.Utils.Parent;
                path.Add(node);
            }

            path.Reverse();
            return path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        /// <returns></returns>
        public static List<INode> BiBacktrace(Node nodeA, Node nodeB)
        {
            var pathA = Backtrace(nodeA);
            var pathB = Backtrace(nodeB);
            pathB.Reverse();

            return pathA.Concat(pathB) as List<INode>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int PathLength(List<INode> path)
        {
            var sum = 0;

            for (var i = 0; i < path.Count; i++)
            {
                var a = path[i - 1];
                var b = path[i];

                var dx = a.PosX - b.PosX;
                var dy = a.PosY - b.PosY;

                sum += (int)Math.Sqrt(dx * dx + dy * dy);
            }

            return sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        public static List<INode> Interpolate(int x0, int y0, int x1, int y1)
        {
            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);

            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;

            var err = dx - dy;

            var line = new List<INode>();

            while (true)
            {
                line.Add(new Node(x0, y0));

                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > -dy)
                {
                    err = err - dy;
                    x0 = x0 + sx;
                }

                if (e2 < dx)
                {
                    err = err + dx;
                    y0 = y0 + sy;
                }
            }

            return line;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<INode> ExpandPath(List<INode> path)
        {
            var len = path.Count;
            var expanded = new List<INode>();

            if (len < 2)
            {
                return expanded;
            }

            for (var i = 0; i < len - 1; i++)
            {
                var nodeA = path[i];
                var nodeB = path[i + 1];

                var interpolated = Interpolate(nodeA.PosX, nodeA.PosY, nodeB.PosX, nodeB.PosY);
                var interpolatedLength = interpolated.Count;

                for (var j = 0; j < interpolatedLength - 1; j++)
                {
                    expanded.Add(interpolated[j]);
                }
            }

            expanded.Add(path[len - 1]);
            return expanded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<INode> SmoothenPath(IGrid grid, List<INode> path)
        {
            var len = path.Count;

            var startingNode = path[0];
            var endNode = path[len - 1];
            var newPath = new List<INode> {startingNode};

            for (var i = 2; i < len; i++)
            {
                var node = path[i];
                var line = Interpolate(startingNode.PosX, startingNode.PosY, node.PosX, node.PosY);

                var blocked = false;

                for (var j = 1; j < line.Count; j++)
                {
                    var testNode = line[j];

                    if (!grid.IsWalkableAt(testNode.PosX, testNode.PosY))
                    {
                        blocked = true;
                        break;
                    }
                }

                if (blocked)
                {
                    var lastValidNode = path[i -1];
                    newPath.Add(lastValidNode);

                    startingNode = lastValidNode;
                }
            }

            newPath.Add(endNode);
            return newPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<INode> CompressPath(List<INode> path)
        {
            if (path.Count < 3)
            {
                return path;
            }

            var compressed = new List<INode>();

            var nodeA = path[0];
            var nodeB = path[1];

            var dx = nodeB.PosX - nodeA.PosX;
            var dy = nodeB.PosY - nodeA.PosY;

            var sq = (int)Math.Sqrt(dx*dx + dy*dy);
            dx /= sq;
            dy /= sq;

            compressed.Add(nodeA);

            for (var i = 2; i < path.Count; i++)
            {
                var previousNode = nodeB;

                var ldx = dx;
                var ldy = dy;

                nodeB = path[i];

                dx = nodeB.PosX - previousNode.PosX;
                dy = nodeB.PosY - previousNode.PosY;

                sq = (int)Math.Sqrt(dx * dx + dy * dy);
                dx /= sq;
                dy /= sq;

                if (dx != ldx || dy != ldy)
                {
                    compressed.Add(previousNode);
                }
            }

            compressed.Add(nodeB);
            return compressed;
        }
    }
}