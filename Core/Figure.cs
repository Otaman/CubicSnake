namespace CubicSnake.Core
{
    public class Figure
    {
        public Figure(Segment[] segments)
        {
            Segments = segments;
        }

        public Segment[] Segments { get; }

        public bool FitInSpace(Space space)
        {
            var previousSegment = new Segment(new Point(0, 0, 0), new Point(0, 0, 1));
            
            var tailOfFirstSegment = Segments[0].Tail;
            space.Insert(tailOfFirstSegment);

            return FitSegmentsInSpace(space, previousSegment, 0);
        }

        public bool FitSegmentsInSpace(Space space, Segment previousSegment, int indexOfCurrentSegment)
        {
            if (indexOfCurrentSegment >= Segments.Length)
                return true;

            var segment = Segments[indexOfCurrentSegment];
            var nextSegmentsDeferredRotations = 0;
            for (int rotation = 0; rotation < 4; rotation++)
            {
                if (FitSegmentInSpace(space, segment))
                {
                    for (; nextSegmentsDeferredRotations > 0; nextSegmentsDeferredRotations--)
                    {
                        RotateNextSegments(previousSegment, indexOfCurrentSegment);
                    }

                    if (FitSegmentsInSpace(space, segment, indexOfCurrentSegment + 1))
                    {
                        Segments[indexOfCurrentSegment] = segment;
                        return true;
                    }
                }

                segment = segment.Rotate(previousSegment, 90);
                nextSegmentsDeferredRotations++;
            }

            nextSegmentsDeferredRotations = nextSegmentsDeferredRotations % 4;
            for (; nextSegmentsDeferredRotations > 0; nextSegmentsDeferredRotations--)
            {
                RotateNextSegments(previousSegment, indexOfCurrentSegment);
            }
            
            return false;
        }

        private void RotateNextSegments(Segment previousSegment, int indexOfCurrentSegment)
        {
            for (int i = indexOfCurrentSegment + 1; i < Segments.Length; i++)
            {
                Segments[i] = Segments[i].Rotate(previousSegment, 90);
            }
        }

        private bool FitSegmentInSpace(Space space, Segment segment)
        {
            //from head to tail
            var points = segment.GetPoints();

            for (int i = 0; i < points.Length - 1; i++) // without tail point
            {
                if (space.Insert(points[i]))
                {
                    continue; // try insert next
                }

                //rollback
                i--;
                for (; i >= 0; i--)
                {
                    space.Remove(points[i]);
                }

                return false;
            }

            return true;
        }
    }
}