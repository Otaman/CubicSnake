using System;

namespace CubicSnake.Core
{
    internal static class LinearAlgebra
    {
        public static void ValidateAngle(int angle)
        {
            if(angle < -360 || angle > 360)
                throw new ArgumentException("Angle must be in range from -360 to 360");
            if(angle % 90 != 0)
                throw new ArgumentException("Angle must steps of 90 degrees");
        }

        public static int[] GetVector(Segment axcis)
        {
            var vectorPoint = Translate(axcis.Head, Reflect(axcis.Tail));
            
            return new []{vectorPoint.X, vectorPoint.Y, vectorPoint.Z};
        }

        public static Point Translate(Point point, Point delta)
        {
            return new Point(point.X + delta.X, 
                point.Y + delta.Y,
                point.Z + delta.Z);
        }

        public static Point Reflect(Point point)
        {
            return new Point(-point.X, -point.Y, -point.Z);
        }

        public static int[,] GetRotationMatrix(int[] vector, int angle)
        {
            var x = vector[0];
            var y = vector[1];
            var z = vector[2];
            
            var sin = Sin(angle);
            var cos = Cos(angle);
            
            var result = new int[,]
            {
                {cos + x*x*(1-cos), x*y*(1-cos) - z*sin, x*z*(1-cos) + y*sin},
                {y*x*(1-cos) + z*sin, cos + y*y*(1-cos), y*z*(1-cos) - x*sin},
                {z*x*(1-cos) - y*sin, z*y*(1-cos) + x*sin, cos + z*z*(1-cos)},
            };
            return result;
        }

        private static int Sin(int angle)
        {
            if (angle == 90)
                return 1;
            else
                throw new NotImplementedException();
        }
        
        private static int Cos(int angle)
        {
            if (angle == 90)
                return 0;
            else
                throw new NotImplementedException();
        }

        public static Point Rotate(Point point, int[,] rotationMatrix)
        {
            var vector = new[] {point.X, point.Y, point.Z};
            var result = new int[3];
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i] += rotationMatrix[i, j] * vector[j];
                }
            }
            
            return new Point(result[0], result[1], result[2]);
        }
    }
}