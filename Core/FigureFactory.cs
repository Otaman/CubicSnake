namespace CubicSnake.Core
{
    public class FigureFactory
    {
        public Figure GenerateFromSegmentLengthsSequence(int[] segmentLengths)
        {
            var startPoint = Point.Zero;
            
            var segments = new Segment[segmentLengths.Length];
            
            //from tail to head segments generation
            var lastHead = startPoint;
            var moveHeadByX = true;
            for (int i = 0; i < segmentLengths.Length; i++)
            {
                var segmentLength = segmentLengths[i];
                var shift = segmentLength - 1;

                var xShift = moveHeadByX ? shift : 0;
                var yShift = moveHeadByX ? 0 : shift;

                var head = LinearAlgebra.Translate(lastHead, new Point(xShift, yShift, 0));
                
                var segment = new Segment(lastHead, head);
                segments[i] = segment;

                lastHead = head;
                //switch moving by other vertex
                moveHeadByX = !moveHeadByX;
            }
            
            return new Figure(segments);
        }
    }
}