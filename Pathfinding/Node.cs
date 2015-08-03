using System;
using Pathfinding.Utils;

namespace Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public class Node : INode, IComparable<INode>
    {
        /// <summary>
        /// The X position within the grid.
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// The Y position within the grid.
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// Specifies if the node is walkable or not.
        /// </summary>
        public bool IsWalkable { get; set; }

        /// <summary>
        /// The node utilities <see cref="NodeUtils"/>.
        /// </summary>
        public NodeUtils Utils { get; }

        /// <summary>
        /// Node Constructor, sets the position of the node and defaults
        /// walkable to be true. This also creates a new NodeUtils that
        /// can be referenced externally.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWalkable"></param>
        public Node(int x, int y, bool isWalkable = true)
        {
            PosX = x;
            PosY = y;
            IsWalkable = isWalkable;
            Utils = new NodeUtils();
        }

        public int CompareTo(INode other)
        {
            if (other == null)
                return 1;

            return (int)Utils.F - (int)other.Utils.F;
        }
    }
}