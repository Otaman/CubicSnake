namespace CubicSnake.Core
{
    /// <summary>
    /// Point represents single point in 3d space
    /// </summary>
    /// <typeparam name="T">
    /// Type for storing point coordinates
    /// Constraint struct type only for ability use ValuteType.Equals fast byte-to-byte comparison
    /// </typeparam>
    public struct Point<T> where T : struct 
    {
        public Point(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public T X { get; }
        public T Y { get; }
        public T Z { get; }
    }
}