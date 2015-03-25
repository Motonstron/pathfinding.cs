using System;
using System.Collections;
using System.Collections.Generic;

namespace PathFinding {

	public class Grid : IGrid {

		private int _width;

		private int _height;

		private int[][] _matrix;

		private INode[][] _nodes;

		/// <summary>
		/// Initializes a new instance of the <see cref="Grid"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="matrix">Matrix.</param>
		public Grid(int width, int height, int[][] matrix = null) {

			Width = width;
			Height = height;

			_matrix = matrix;
			_nodes = BuildNodes();
		}

		/// <summary>
		/// Builds the nodes.
		/// </summary>
		/// <returns>The nodes.</returns>
		private INode[][] BuildNodes() {

			INode[][] nodes = new INode[_height][];
			var i = 0;
			var j = 0;

			for(i = 0; i < _height; i++) {
				nodes[i] = new INode[_width];
				for(j = 0; j < _width; j++) {
					nodes[i][j] = new INode(j, i);
				}
			}

			if(_matrix == null)
				return nodes;

			if(_matrix.Length != _height || _matrix[0].Length != _width) {
				throw new System.InvalidOperationException("Matrix size does not fit");
			}

			for(i = 0; i < _height; i++) {
				for(j = 0; j < _width; j++) {
					nodes[i][j].IsWalkable = _matrix[i][j] == 0;
				}
			}

			return nodes;
		}

		/// <summary>
		/// Gets the neighbours.
		/// </summary>
		/// <returns>The neighbours.</returns>
		/// <param name="node">Node.</param>
		/// <param name="diagonalMovement">Diagonal movement.</param>
		public List<INode> GetNeighbours(INode node, DiagonalMovement diagonalMovement) {
			var x = node.PosX;
			var y = node.PosY;

			List<INode> neighbours = new List<INode>();

			bool s0 = false, d0 = false,
				s1 = false, d1 = false,
				s2 = false, d2 = false,
				s3 = false, d3 = false;

			// ↑
			if(IsWalkableAt(x, y - 1)) {
				neighbours.Add(_nodes[y - 1][x]);
				s0 = true;
			}

			// →
			if(IsWalkableAt(x + 1, y)) {
				neighbours.Add(_nodes[y][x + 1]);
				s1 = true;
			}

			// ↓
			if(IsWalkableAt(x, y + 1)) {
				neighbours.Add(_nodes[y + 1][x]);
				s2 = true;
			}

			// ←
			if(IsWalkableAt(x - 1, y)) {
				neighbours.Add(_nodes[y][x - 1]);
				s3 = true;
			}

			if(diagonalMovement == DiagonalMovement.NEVER)
				return neighbours;

			if(diagonalMovement == DiagonalMovement.ONLY_WHEN_NO_OBSTACLES) {
				d0 = s3 && s0;
				d1 = s0 && s1;
				d2 = s1 && s2;
				d3 = s2 && s3;
			} else if(diagonalMovement == DiagonalMovement.IF_AT_MOST_ONE_OBSTACLE) {
				d0 = s3 || s0;
				d1 = s0 || s1;
				d2 = s1 || s2;
				d3 = s2 || s3;
			} else if(diagonalMovement == DiagonalMovement.ALWAYS) {
				d0 = true;
				d1 = true;
				d2 = true;
				d3 = true;
			} else {
				throw new System.InvalidOperationException("Incorrect Diagonal Movement");
			}

			// ↖
			if(d0 && IsWalkableAt(x - 1, y - 1)) {
				neighbours.Add(_nodes[y - 1][x - 1]);
			}

			// ↗
			if(d1 && IsWalkableAt(x + 1, y - 1)) {
				neighbours.Add(_nodes[y - 1][x + 1]);
			}

			// ↘
			if(d2 && IsWalkableAt(x + 1, y + 1)) {
				neighbours.Add(_nodes[y + 1][x + 1]);
			}

			// ↙
			if(d3 && IsWalkableAt(x - 1, y + 1)) {
				neighbours.Add(_nodes[y + 1][x - 1]);
			}

			return neighbours;
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public IGrid Clone() {
			var newGrid = new Grid(_width, _height);
			var newNodes = new INode[_height][];

			for(var i = 0; i < _height; i++) {
				newNodes[i] = new INode[_width];
				for(var j = 0; j < _width; j++) {
					newNodes[i][j]  = new Node(j, i, _nodes[i][j].IsWalkable);
				}
			}

			newGrid.SetNodes(newNodes);

			return newGrid;
		}

		/// <summary>
		/// Sets the walkable at posX, posY and isWalkable.
		/// </summary>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		/// <param name="isWalkable">If set to <c>true</c> is walkable.</param>
		public void SetWalkableAt(int posX, int posY, bool isWalkable) {
			_nodes[posY][posX].IsWalkable = isWalkable;
		}

		/// <summary>
		/// Determines whether this instance is walkable at the specified posX posY.
		/// </summary>
		/// <returns><c>true</c> if this instance is walkable at the specified posX posY; otherwise, <c>false</c>.</returns>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		public bool IsWalkableAt(int posX, int posY) {
			return IsInside(posX, posY) && _nodes[posY][posX].IsWalkable;
		}

		/// <summary>
		/// Gets the node at posX and posY.
		/// </summary>
		/// <returns>The <see cref="INode"/>.</returns>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		public INode GetNodeAt(int posX, int posY) {
			return _nodes[posY][posX];
		}

		/// <summary>
		/// Determines whether this instance is inside the specified posX posY.
		/// </summary>
		/// <returns><c>true</c> if this instance is inside the specified posX posY; otherwise, <c>false</c>.</returns>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		public bool IsInside(int posX, int posY) {
			return (posX >= 0 && posX < _width) && (posY >= 0 && posY < _height);
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>The width.</value>
		public int Width {
			get {
				return _width;
			}
			private set {
				_width = value;
			}
		}

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>The height.</value>
		public int Height {
			get {
				return _height;
			}
			private set {
				_height = value;
			}
		}

		/// <summary>
		/// Sets the nodes.
		/// </summary>
		/// <param name="nodes">Nodes.</param>
		private void SetNodes(INode[][] nodes) {
			_nodes = nodes;
		}
	}
}
