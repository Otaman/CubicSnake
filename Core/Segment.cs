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
            var tail = Tail.Rotate(axcis, angle);
            var head = Head.Rotate(axcis, angle);

            return new Segment(tail, head);
        }

        public Point[] GetPoints()
        {
            throw new NotImplementedException();
        }
    }
}