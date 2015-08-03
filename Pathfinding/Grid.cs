using System;
using System.Collections.Generic;

namespace Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public class Grid : IGrid
    {
        /// <summary>
        /// The number of columns in the grid.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
        public int Rows { get; }

        private INode[][] _nodes;

        private readonly int[][] _matrix;

        /// <summary>
        /// Grid Constructor, specifies the number of columns and rows, as well as a
        /// matrix to specify walkable and non-walkable nodes.
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="matrix"></param>
        public Grid(int columns, int rows, int[][] matrix = null)
        {
            Columns = columns;
            Rows = rows;

            _matrix = matrix;
            _nodes = BuildNodes();
        }

        /// <summary>
        /// Grid Constructor, take and a matrix that specifies the walkable and non-walkable
        /// nodes, the number of columns and rows are also inferred from the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        public Grid(int[][] matrix)
        {
            Columns = matrix[0].Length;
            Rows = matrix.Length;

            _matrix = matrix;
            _nodes = BuildNodes();
        }

        /// <summary>
        /// Sets the node at a specified co-ordinate walkable true or false.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWalkable"></param>
        public void SetWalkableAt(int x, int y, bool isWalkable)
        {
            if (!IsInside(x, y))
            {
                throw new Exception(string.Format("You are trying to set node {0}x{1}'s walkable state when it is outside of the grid boundaries.", x, y));
            }

            _nodes[y][x].IsWalkable = isWalkable;
        }

        /// <summary>
        /// Returns if the node is walkable true or false.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns true / false.</returns>
        public bool IsWalkableAt(int x, int y)
        {
            return IsInside(x, y) && _nodes[y][x].IsWalkable;
        }

        /// <summary>
        /// Checks if a node is inside of the grid boundaries.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns true / false.</returns>
        public bool IsInside(int x, int y)
        {
            return (x >= 0 && x < Columns) && (y >= 0 && y < Rows);
        }

        /// <summary>
        /// Returns the node at the specified position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns <see cref="INode"/>.</returns>
        public INode GetNodeAt(int x, int y)
        {
            if (!IsInside(x, y))
            {
                throw new Exception(string.Format("You are trying to retrieve node {0}x{1} when it is outside of the grid boundaries.", x, y));
            }

            return _nodes[y][x];
        }

        /// <summary>
        /// Clones the grid in its current state.
        /// </summary>
        /// <returns>Returns <see cref="IGrid"/>.</returns>
        public IGrid Clone()
        {
            var newGrid = new Grid(Columns, Rows);
            var newNodes = new INode[Rows][];

            for (var i = 0; i < Rows; i++)
            {
                newNodes[i] = new INode[Columns];
                for (var j = 0; j < Columns; j++)
                {
                    newNodes[i][j] = new Node(j, i, _nodes[i][j].IsWalkable);
                }
            }

            newGrid.SetNodes(newNodes);
            return newGrid;
        }

        /// <summary>
        /// Get the neighbors of the given node.
        /// 
        /// offsets diagonalOffsets:
        /// +---+---+---+    +---+---+---+
        /// |   | 0 |   |    | 0 |   | 1 |
        /// +---+---+---+    +---+---+---+
        /// | 3 |   | 1 |    |   |   |   |
        /// +---+---+---+    +---+---+---+
        /// |   | 2 |   |    | 3 |   | 2 |
        /// +---+---+---+    +---+---+---+
        /// 
        /// When allowDiagonal is true, if offsets[i] is valid, then
        /// diagonalOffsets[i] anddiagonalOffsets[(i + 1) % 4] is valid.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="diagonalMovement"></param>
        /// <returns>Returns List of type <see cref="INode"/>.</returns>
        public List<INode> GetNeighborsForNode(INode node, DiagonalMovement diagonalMovement)
        {
            var x = node.PosX;
            var y = node.PosY;

            var neighbors = new List<INode>();

            bool s0 = false, d0,
                s1 = false, d1,
                s2 = false, d2,
                s3 = false, d3;

            // ↑
            if (IsWalkableAt(x, y - 1))
            {
                neighbors.Add(_nodes[y - 1][x]);
                s0 = true;
            }

            // →
            if (IsWalkableAt(x + 1, y))
            {
                neighbors.Add(_nodes[y][x + 1]);
                s1 = true;
            }

            // ↓
            if (IsWalkableAt(x, y + 1))
            {
                neighbors.Add(_nodes[y + 1][x]);
                s2 = true;
            }

            // ←
            if (IsWalkableAt(x - 1, y))
            {
                neighbors.Add(_nodes[y][x - 1]);
                s3 = true;
            }

            switch (diagonalMovement)
            {
                case DiagonalMovement.Never:
                    return neighbors;
                case DiagonalMovement.OnlyWhenNoObstacles:
                    d0 = s3 && s0;
                    d1 = s0 && s1;
                    d2 = s1 && s2;
                    d3 = s2 && s3;
                    break;
                case DiagonalMovement.IfAtMostOneObstacle:
                    d0 = s3 || s0;
                    d1 = s0 || s1;
                    d2 = s1 || s2;
                    d3 = s2 || s3;
                    break;
                case DiagonalMovement.Always:
                    d0 = true;
                    d1 = true;
                    d2 = true;
                    d3 = true;
                    break;
                default:
                    throw new InvalidOperationException("Incorrect Diagonal Movement");
            }

            // ↖
            if (d0 && IsWalkableAt(x - 1, y - 1))
            {
                neighbors.Add(_nodes[y - 1][x - 1]);
            }

            // ↗
            if (d1 && IsWalkableAt(x + 1, y - 1))
            {
                neighbors.Add(_nodes[y - 1][x + 1]);
            }

            // ↘
            if (d2 && IsWalkableAt(x + 1, y + 1))
            {
                neighbors.Add(_nodes[y + 1][x + 1]);
            }

            // ↙
            if (d3 && IsWalkableAt(x - 1, y + 1))
            {
                neighbors.Add(_nodes[y + 1][x - 1]);
            }

            return neighbors;
        }

        private INode[][] BuildNodes()
        {
            var nodes = new INode[Rows][];

            int i;
            int j;

            for (i = 0; i < Rows; i++)
            {
                nodes[i] = new INode[Columns];
                for (j = 0; j < Columns; j++)
                {
                    nodes[i][j] = new Node(j, i);
                }
            }

            if (_matrix == null)
                return nodes;

            if (_matrix.Length != Rows || _matrix[0].Length != Columns)
            {
                throw new InvalidOperationException("Matrix size does not fit");
            }

            for (i = 0; i < Rows; i++)
            {
                for (j = 0; j < Columns; j++)
                {
                    nodes[i][j].IsWalkable = _matrix[i][j] == 0;
                }
            }

            return nodes;
        }

        private void SetNodes(INode[][] nodes)
        {
            _nodes = nodes;
        }
    }
}