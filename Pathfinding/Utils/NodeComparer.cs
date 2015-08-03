using System;
using System.Collections.Generic;

namespace Pathfinding.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class NodeComparer : Comparer<INode>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(INode x, INode y)
        {
            return (int)x.Utils.F - (int)y.Utils.F;
        }
    }
}