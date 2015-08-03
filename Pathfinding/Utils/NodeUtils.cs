namespace Pathfinding.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public enum SearchState
    {
        Undefined,
        Closed,
        Open
    }

    /// <summary>
    /// 
    /// </summary>
    public class NodeUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public float F { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public float G { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public float H { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public INode Parent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SearchState SearchState = SearchState.Undefined;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void UpdateF(float value)
        {
            F = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void UpdateG(float value)
        {
            G = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void UpdateH(float value)
        {
            H = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void UpdateParent(INode node)
        {
            Parent = node;
        }
    }
}