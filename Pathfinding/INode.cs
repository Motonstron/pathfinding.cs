using Pathfinding.Utils;

namespace Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// 
        /// </summary>
        int PosX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int PosY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsWalkable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        NodeUtils Utils { get; }
    }
}