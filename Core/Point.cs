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

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }
        
        public Point Rotate(Segment axcis, int angle)
        {
            LinearAlgebra.ValidateAngle(angle);

            var vector = LinearAlgebra.GetVector(axcis);
            var rotationMatrix = LinearAlgebra.GetRotationMatrix(vector, angle);

            var translatedPoint = LinearAlgebra.Translate(this, LinearAlgebra.Reflect(axcis.Tail));
            var rotatedPoint = LinearAlgebra.Rotate(translatedPoint, rotationMatrix);
            var retranslatedPoint = LinearAlgebra.Translate(rotatedPoint, axcis.Tail);

            return retranslatedPoint;
        }
    }
}