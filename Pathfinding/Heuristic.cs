using System;

namespace Pathfinding
{
    /// <summary>
    /// 
    /// </summary>
    public enum HeuristicMode
    {
        Manhattan,
        Euclidean,
        Octile,
        ChebyShev
    }

    /// <summary>
    /// 
    /// </summary>
    public class Heuristic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static int Manhattan(int dx, int dy)
        {
            return dx + dy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static int Euclidean(int dx, int dy)
        {
            return (int)Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static int Octile(int dx, int dy)
        {
            var f = (float)Math.Sqrt(2) - 1;
            return (dx < dy) ? (int)f * dx + dy : (int)f * dy + dx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static int ChebyShev(int dx, int dy)
        {
            return Math.Max(dx, dy);
        }
    }
}