using System.Collections.Generic;

namespace Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// 
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// 
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWalkable"></param>
        void SetWalkableAt(int x, int y, bool isWalkable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsWalkableAt(int x, int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsInside(int x, int y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        INode GetNodeAt(int x, int y);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IGrid Clone();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="diagonalMovement"></param>
        /// <returns></returns>
        List<INode> GetNeighborsForNode(INode node, DiagonalMovement diagonalMovement);
    }
}