using System;
using System.Diagnostics;

namespace CubicSnake.Core
{
    /// <summary>
    /// Segment is straight line between two points with direction from tail to head
    /// </summary>
    [DebuggerDisplay("[T{Tail}-H{Head}]")]
    public struct Segment : I3DRotatable<Segment>, I3DTranslatable<Segment>
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
            var tail = Tail.Rotate(axcis, angle);
            var head = Head.Rotate(axcis, angle);

            return new Segment(tail, head);
        }

        public Point[] GetPoints()
        {
            var difference = LinearAlgebra.Translate(Head, LinearAlgebra.Reflect(Tail));
            var distance = Math.Abs(difference.X) + Math.Abs(difference.Y) + Math.Abs(difference.Z);
            
            var normalizedDifference = new Point(difference.X/distance, difference.Y/distance, difference.Z/distance);

            var result = new Point[distance+1];
            result[0] = Head;
            result[result.Length - 1] = Tail;

            var step = LinearAlgebra.Reflect(normalizedDifference); //inverted difference for going from head to tail

            for (int i = 1; i < result.Length-1; i++)
            {
                result[i] = LinearAlgebra.Translate(result[i - 1], step);
            }

            return result;
        }

        public Segment Translate(Point point)
        {
            return new Segment(Tail.Translate(point), Head.Translate(point));
        }

        public override string ToString()
        {
            return $"[{Tail}-{Head}]";
        }
    }
}