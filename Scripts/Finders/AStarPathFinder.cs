using System.Collections;
using System.Collections.Generic;

namespace PathFinding {
	public class AStarPathFinder {

		private HeuristicMode _heuristicMode;

		private DiagonalMovement _diagonalMovement;

		private int _weight;

		/// <summary>
		/// Initializes a new instance of the <see cref="AStarPathFinder"/> class.
		/// </summary>
		/// <param name="allowDiagonal">If set to <c>true</c> allow diagonal.</param>
		/// <param name="dontCrossCorners">If set to <c>true</c> dont cross corners.</param>
		/// <param name="diagonalMovement">Diagonal movement.</param>
		/// <param name="heuristic">Heuristic.</param>
		/// <param name="weight">Weight.</param>
		public AStarPathFinder(bool allowDiagonal, bool dontCrossCorners, DiagonalMovement diagonalMovement, HeuristicMode heuristic = HeuristicMode.Manhattan, int weight = 1) {

			_diagonalMovement = diagonalMovement;
			_weight = weight;

			if(!allowDiagonal) {
				_diagonalMovement = DiagonalMovement.NEVER;
			} else {
				if(dontCrossCorners) {
					_diagonalMovement = DiagonalMovement.ONLY_WHEN_NO_OBSTACLES;
				} else {
					_diagonalMovement = DiagonalMovement.IF_AT_MOST_ONE_OBSTACLE;
				}
			}
		}

		/// <summary>
		/// Finds the path.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="startX">Start x.</param>
		/// <param name="startY">Start y.</param>
		/// <param name="endX">End x.</param>
		/// <param name="endY">End y.</param>
		/// <param name="grid">Grid.</param>
		public List<INode> FindPath(int startX, int startY, int endX, int endY, IGrid grid) {

			var path = new List<INode>();
			var openList = new HeapQ<INode>(new NodeComparer());

			var startNode = grid.GetNodeAt(startX, startY);
			var endNode = grid.GetNodeAt(endX, endY);

			openList.Push(startNode);
			startNode.Utils.searchState = SearchState.Opened;

			startNode.Utils.g = 0;
			startNode.Utils.f = 0;
			
			while(!openList.Empty) {

				var node = openList.Pop();
				node.Utils.searchState = SearchState.Closed;

				if(node == endNode) {
					return PathFindingUtils.Backtrace(endNode);
				}

				var neighbours = grid.GetNeighbours(node, _diagonalMovement);
				foreach(INode neighbourNode in neighbours) {
					var neighbour = neighbourNode;

					if(neighbour.Utils.searchState == SearchState.Closed) {
						continue;
					}

					var posX = neighbour.PosX;
					var posY = neighbour.PosY;

					var ng = node.Utils.g + ((posX - node.PosX == 0 || posY - node.PosY == 0) ? 1 : Mathf.Sqrt(2));

					if(neighbour.Utils.searchState != SearchState.Opened || ng < neighbour.Utils.g) {
						neighbour.Utils.g = ng;
						neighbour.Utils.h = _weight * Heuristic.Manhattan(Mathf.Abs(posX - endX), Mathf.Abs(posY - endY));
						neighbour.Utils.f = neighbour.Utils.g + neighbour.Utils.h;
						neighbour.Utils.parent = node;
						
						if(neighbour.Utils.searchState != SearchState.Opened) {
							openList.Push(neighbour);
							neighbour.Utils.searchState = SearchState.Opened;
						} else {
							openList.UpdateItem(neighbour);
						}
					}
				}
			}
			
			return path;
		}
	}
}