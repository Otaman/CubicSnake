namespace CubicSnake.Core
{
    /// <summary>
    /// Point represents single point in 3d space
    /// </summary>
    public struct Point
    {
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
    }
}