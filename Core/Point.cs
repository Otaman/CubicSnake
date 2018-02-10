using System;
using System.Diagnostics;

namespace CubicSnake.Core
{
    /// <summary>
    /// Point represents single point in 3d space
    /// </summary>
    [DebuggerDisplay("({X},{Y},{Z})")]
    public struct Point : I3DRotatable<Point>, I3DTranslatable<Point>
    {
        public static Point Zero = new Point(0, 0, 0);
        
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
            
            
            var normalizationKoefficient = Math.Abs(vector[0]) + Math.Abs(vector[1]) + Math.Abs(vector[2]);

            vector[0] /= normalizationKoefficient;
            vector[1] /= normalizationKoefficient;
            vector[2] /= normalizationKoefficient;
            
            var rotationMatrix = LinearAlgebra.GetRotationMatrix(vector, angle);

            var translatedPoint = LinearAlgebra.Translate(this, LinearAlgebra.Reflect(axcis.Tail));
            var rotatedPoint = LinearAlgebra.Rotate(translatedPoint, rotationMatrix);
            var retranslatedPoint = LinearAlgebra.Translate(rotatedPoint, axcis.Tail);

            return retranslatedPoint;
        }

        public Point Translate(Point point)
        {
            return LinearAlgebra.Translate(this, point);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}