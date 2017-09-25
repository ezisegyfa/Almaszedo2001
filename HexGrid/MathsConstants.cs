using static System.Math;

namespace TurnBasedTableStrategyGame.HexGrid
{
    /// <summary>
    /// Useful maths constants for drawing hexes
    /// </summary>
    public static class MathsConstants
    {
        /// <summary>
        /// The Sine of 30 degrees 
        /// </summary>
        public static readonly float Sin30 = 0.5F;

        /// <summary>
        /// The Cosine of 30 degrees 
        /// </summary>
        public static readonly float Cos30 = (float)(Sqrt(3) / 2);
    }
}
