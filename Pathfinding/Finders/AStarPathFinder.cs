using System;
using System.Collections.Generic;
using Pathfinding.Utils;

namespace Pathfinding.Finders
{
    /// <summary>
    /// 
    /// </summary>
    public class AStarPathFinder
    {
        private HeuristicMode _heuristicMode;

        private readonly DiagonalMovement _diagonalMovement;

        private readonly int _weight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allowDiagonal"></param>
        /// <param name="dontCrossCorners"></param>
        /// <param name="diagonalMovement"></param>
        /// <param name="heuristic"></param>
        /// <param name="weight"></param>
        public AStarPathFinder(bool allowDiagonal, bool dontCrossCorners, DiagonalMovement diagonalMovement, HeuristicMode heuristic = HeuristicMode.Manhattan, int weight = 1)
        {
            _heuristicMode = heuristic;
            _diagonalMovement = diagonalMovement;
            _weight = weight;

            if (!allowDiagonal)
            {
                _diagonalMovement = DiagonalMovement.Never;
            }
            else
            {
                _diagonalMovement = dontCrossCorners ? DiagonalMovement.OnlyWhenNoObstacles : DiagonalMovement.IfAtMostOneObstacle;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<INode> FindPath(int startX, int startY, int endX, int endY, IGrid grid)
        {

            var path = new List<INode>();
            var openList = new HeapQ<INode>(new NodeComparer());

            var startNode = grid.GetNodeAt(startX, startY);
            var endNode = grid.GetNodeAt(endX, endY);

            openList.Push(startNode);
            startNode.Utils.SearchState = SearchState.Open;

            startNode.Utils.UpdateG(0);
            startNode.Utils.UpdateF(0);

            while (!openList.IsEmpty)
            {

                var node = openList.Pop();
                node.Utils.SearchState = SearchState.Closed;

                if (node == endNode)
                {
                    return PathfindingUtils.Backtrace(endNode);
                }

                var neighbours = grid.GetNeighborsForNode(node, _diagonalMovement);
                foreach (var neighbourNode in neighbours)
                {
                    var neighbour = neighbourNode;

                    if (neighbour.Utils.SearchState == SearchState.Closed)
                    {
                        continue;
                    }

                    var posX = neighbour.PosX;
                    var posY = neighbour.PosY;

                    var ng = node.Utils.G + ((posX - node.PosX == 0 || posY - node.PosY == 0) ? 1 : (float)Math.Sqrt(2));

                    if (neighbour.Utils.SearchState == SearchState.Open && !(ng < neighbour.Utils.G))
                        continue;

                    neighbour.Utils.UpdateG(ng);
                    neighbour.Utils.UpdateH(_weight * Heuristic.Manhattan(Math.Abs(posX - endX), Math.Abs(posY - endY)));
                    neighbour.Utils.UpdateF(neighbour.Utils.G + neighbour.Utils.H);
                    neighbour.Utils.UpdateParent(node);

                    if (neighbour.Utils.SearchState != SearchState.Open)
                    {
                        openList.Push(neighbour);
                        neighbour.Utils.SearchState = SearchState.Open;
                    }
                    else
                    {
                        openList.UpdateItem(neighbour);
                    }
                }
            }

            return path;
        }
    }
}