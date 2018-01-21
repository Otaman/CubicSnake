using System;

namespace CubicSnake.Core
{
    /// <summary>
    /// Segment is straight line between two points with direction from tail to head
    /// </summary>
    public struct Segment
    {
        public Segment(Point tail, Point head)
        {
            if (tail == head)
                throw new ArgumentException("Tail and head must be not equals");
            
            Tail = tail;
            Head = head;
        }

        public Point Tail { get; }
        public Point Head { get; }

        public Segment Rotate(Segment axcis, int angle)
        {
            if(angle < -360 || angle > 360)
                throw new ArgumentException("Angle must be in range from -360 to 360");
            if(angle % 90 != 0)
                throw new ArgumentException("Angle must steps of 90 degrees");

            var rotationMatrix = new int[,]
            {
                {1, 0, 0},
                {0, 0, -1},
                {0, 1, 0}
            };

            var input = new int[] {Head.X, Head.Y, Head.Z};
            var result = new int[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i] += rotationMatrix[i, j] * input[j];
                }
            }
            
            return new Segment(axcis.Tail, new Point(result[0], result[1], result[2]));
        }
    }
}